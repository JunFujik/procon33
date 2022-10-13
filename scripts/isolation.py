#マルチチャンネル時変ガウスモデル

import wave as wave
import pyroomacoustics as pa
import numpy as np
import scipy.signal as sp
import scipy as scipy

#順列計算に使用
import itertools
import time



#コントラスト関数の微分（球対称多次元ラプラス分布を仮定）
#s_hat: 分離信号(M, Nk, Lt)
def phi_multivariate_laplacian(s_hat):

    power=np.square(np.abs(s_hat))
    norm=np.sqrt(np.sum(power,axis=1,keepdims=True))

    phi=s_hat/np.maximum(norm,1.e-18)
    return(phi)

#コントラスト関数の微分（球対称ラプラス分布を仮定）
#s_hat: 分離信号(M, Nk, Lt)
def phi_laplacian(s_hat):

    norm=np.abs(s_hat)
    phi=s_hat/np.maximum(norm,1.e-18)
    return(phi)

#コントラスト関数（球対称ラプラス分布を仮定）
#s_hat: 分離信号(M, Nk, Lt)
def contrast_laplacian(s_hat):

    norm=2.*np.abs(s_hat)
    
    return(norm)

#コントラスト関数（球対称多次元ラプラス分布を仮定）
#s_hat: 分離信号(M, Nk, Lt)
def contrast_multivariate_laplacian(s_hat):
    power=np.square(np.abs(s_hat))
    norm=2.*np.sqrt(np.sum(power,axis=1,keepdims=True))

    return(norm)



#EM法によるLGMのパラメータ推定法
#x:入力信号( M, Nk, Lt)
#Ns: 音源数
#n_iterations: 繰り返しステップ数
#return R 共分散行列(Nk,Ns,M,M) v 時間周波数分散(Nk,Ns,Lt),c_bar 音源分離信号(M,Ns,Nk,Lt), cost_buff コスト (T)
def execute_em_lgm(x,Ns=2,n_iterations=20):
    
    #マイクロホン数・周波数・フレーム数を取得する
    M=np.shape(x)[0]
    Nk=np.shape(x)[1]
    Lt=np.shape(x)[2]

    #Rとvを初期化する
    mask=np.random.uniform(size=Nk*Ns*Lt)
    mask=np.reshape(mask,(Nk,Ns,Lt))
    R=np.einsum("kst,mkt,nkt->kstmn",mask,x,np.conjugate(x))
    R=np.average(R,axis=2)
    v=np.random.uniform(size=Nk*Ns*Lt)
    v=np.reshape(v,(Nk,Ns,Lt))

    cost_buff=[]
    for t in range(n_iterations):
        print(t)
        
        #入力信号の共分散行列を求める
        vR=np.einsum("kst,ksmn->kstmn",v,R)
        V=np.sum(vR,axis=1)
        V_inverse=np.linalg.pinv(V)

        #コスト計算
        cost=np.sum(np.einsum("mkt,ktmn,nkt->kt",np.conjugate(x),V_inverse,x) +np.log(np.abs(np.linalg.det(V))))
        cost/=float(Lt)
        cost=np.real(cost)
        cost_buff.append(cost)

        Wmwf=np.einsum("kstmi,ktin->kstmn",vR,V_inverse)

        #事後確率計算に必要なパラメータを推定
        c_bar=np.einsum('kstmn,nkt->kstm',Wmwf,x)
        R_bar=np.einsum("kstmi,kstin->kstmn",-1.*Wmwf+np.eye(M),vR)
        P_bar=R_bar+np.einsum("kstm,kstn->kstmn",c_bar,np.conjugate(c_bar))
        
        #パラメータを更新
        R=np.average(P_bar/np.maximum(v,1.e-18)[...,None,None],axis=2)

        R_inverse=np.linalg.pinv(R)
        v=np.einsum("ksmi,kstim->kst",R_inverse,P_bar)
        v=v/float(M)


    vR=np.einsum("kst,ksmn->kstmn",v,R)
    V=np.sum(vR,axis=1)
    V_inverse=np.linalg.pinv(V)
    Wmwf=np.einsum("kstmi,ktin->kstmn",vR,V_inverse)

    #音源分離信号を得る
    c_bar=np.einsum('kstmn,nkt->mskt',Wmwf,x)

    return(R,v,c_bar,cost_buff)



#周波数間の振幅相関に基づくパーミュテーション解法
#s_hat: M,Nk,Lt
#return permutation_index_result：周波数毎のパーミュテーション解 
def solver_inter_frequency_permutation(s_hat):
    n_sources=np.shape(s_hat)[0]
    n_freqs=np.shape(s_hat)[1]
    n_frames=np.shape(s_hat)[2]

    s_hat_abs=np.abs(s_hat)

    norm_amp=np.sqrt(np.sum(np.square(s_hat_abs),axis=0,keepdims=True))
    s_hat_abs=s_hat_abs/np.maximum(norm_amp,1.e-18)

    spectral_similarity=np.einsum('mkt,nkt->k',s_hat_abs,s_hat_abs)
    
    frequency_order=np.argsort(spectral_similarity)
    
    #音源間の相関が最も低い周波数からパーミュテーションを解く
    is_first=True
    permutations=list(itertools.permutations(range(n_sources)))
    permutation_index_result={}
    
    for freq in frequency_order:
        
        if is_first==True:
            is_first=False

            #初期値を設定する
            accumurate_s_abs=s_hat_abs[:,frequency_order[0],:]
            permutation_index_result[freq]=range(n_sources)
        else:
            max_correlation=0
            max_correlation_perm=None
            for perm in permutations:
                s_hat_abs_temp=s_hat_abs[list(perm),freq,:]
                correlation=np.sum(accumurate_s_abs*s_hat_abs_temp)
                
                
                if max_correlation_perm is None:
                    max_correlation_perm=list(perm)
                    max_correlation=correlation
                elif max_correlation < correlation:
                    max_correlation=correlation
                    max_correlation_perm=list(perm)
            permutation_index_result[freq]=max_correlation_perm
            accumurate_s_abs+=s_hat_abs[max_correlation_perm,freq,:]

    return(permutation_index_result)



#2バイトに変換してファイルに保存
#signal: time-domain 1d array (float)
#file_name: 出力先のファイル名
#sample_rate: サンプリングレート
def write_file_from_time_signal(signal,file_name,sample_rate):
    #2バイトのデータに変換
    signal=signal.astype(np.int16)

    #waveファイルに書き込む
    wave_out = wave.open(file_name, 'w')

    #モノラル:1、ステレオ:2
    wave_out.setnchannels(1)

    #サンプルサイズ2byte
    wave_out.setsampwidth(2)

    #サンプリング周波数
    wave_out.setframerate(sample_rate)

    #データを書き込み
    wave_out.writeframes(signal)

    #ファイルを閉じる
    wave_out.close()

#SNRをはかる
#desired: 目的音、Lt
#out:　雑音除去後の信号 Lt
def calculate_snr(desired,out):
    wave_length=np.minimum(np.shape(desired)[0],np.shape(out)[0])

    #消し残った雑音
    desired=desired[:wave_length]
    out=out[:wave_length]
    noise=desired-out
    snr=10.*np.log10(np.sum(np.square(desired))/np.sum(np.square(noise)))

    return(snr)

#乱数の種を初期化
np.random.seed(0)

a = input("問題か音源分離した音声ファイルのpath:")
b = input("もう一つの方のpath:")

#畳み込みに用いる音声波形
clean_wave_files=[a, b]

#音源数
n_sources=len(clean_wave_files)

#長さを調べる
n_samples=0
#ファイルを読み込む
for clean_wave_file in clean_wave_files:
    wav=wave.open(clean_wave_file)
    if n_samples<wav.getnframes():
        n_samples=wav.getnframes()
    wav.close()


clean_data=np.zeros([n_sources,n_samples])

#ファイルを読み込む
s=0


for clean_wave_file in clean_wave_files:
    wav=wave.open(clean_wave_file)
    data=wav.readframes(wav.getnframes())
    data=np.frombuffer(data, dtype=np.int16)
    data=data/np.iinfo(np.int16).max
    clean_data[s,:wav.getnframes()]=data
    wav.close()
    s=s+1


# シミュレーションのパラメータ

#シミュレーションで用いる音源数
n_sim_sources=2

#サンプリング周波数
sample_rate=48000

#フレームサイズ
N=4096

#周波数の数
Nk=int(N/2+1)

#各ビンの周波数
freqs=np.arange(0,Nk,1)*sample_rate/N

#音声と雑音との比率 [dB]
SNR=90.

#部屋の大きさ
room_dim = np.r_[100.0, 100.0, 100.0]

#マイクロホンアレイを置く部屋の場所
mic_array_loc = room_dim / 2 + np.random.randn(3) * 0.1 

#マイクロホンアレイのマイク配置
mic_directions=np.array(
    [[np.pi/2., theta/180.*np.pi] for theta in np.arange(180,361,180)
    ]    )

distance=0.01
mic_alignments=np.zeros((3, mic_directions.shape[0]), dtype=mic_directions.dtype)
mic_alignments[0, :] = np.cos(mic_directions[:, 1]) * np.sin(mic_directions[:, 0])
mic_alignments[1, :] = np.sin(mic_directions[:, 1]) * np.sin(mic_directions[:, 0])
mic_alignments[2, :] = np.cos(mic_directions[:, 0])
mic_alignments *= distance

#マイクロホン数
n_channels=np.shape(mic_alignments)[1]

#マイクロホンアレイの座標
R=mic_alignments+mic_array_loc[:,None]

is_use_reverb=False

if is_use_reverb==False:
    # 部屋を生成する
    room = pa.ShoeBox(room_dim, fs=sample_rate, max_order=0) 
    room_no_noise_left = pa.ShoeBox(room_dim, fs=sample_rate, max_order=0)
    room_no_noise_right = pa.ShoeBox(room_dim, fs=sample_rate, max_order=0)

else:

    room = pa.ShoeBox(room_dim, fs=sample_rate, max_order=17,absorption=0.4)
    room_no_noise_left = pa.ShoeBox(room_dim, fs=sample_rate, max_order=17,absorption=0.4)
    room_no_noise_right = pa.ShoeBox(room_dim, fs=sample_rate, max_order=17,absorption=0.4)

# 用いるマイクロホンアレイの情報を設定する
room.add_microphone_array(pa.MicrophoneArray(R, fs=room.fs))
room_no_noise_left.add_microphone_array(pa.MicrophoneArray(R, fs=room.fs))
room_no_noise_right.add_microphone_array(pa.MicrophoneArray(R, fs=room.fs))

#音源の場所
doas=np.array(
    [[np.pi/2., np.pi],
     [np.pi/2., 0]
    ]    )

#音源とマイクロホンの距離
distance=10.

source_locations=np.zeros((3, doas.shape[0]), dtype=doas.dtype)
source_locations[0, :] = np.cos(doas[:, 1]) * np.sin(doas[:, 0])
source_locations[1, :] = np.sin(doas[:, 1]) * np.sin(doas[:, 0])
source_locations[2, :] = np.cos(doas[:, 0])
source_locations *= distance
source_locations += mic_array_loc[:, None]

#各音源をシミュレーションに追加する
for s in range(n_sim_sources):
    clean_data[s]/= np.std(clean_data[s])
    room.add_source(source_locations[:, s], signal=clean_data[s])
    if s==0:
        room_no_noise_left.add_source(source_locations[:, s], signal=clean_data[s])
    if s==1:
        room_no_noise_right.add_source(source_locations[:, s], signal=clean_data[s])

#シミュレーションを回す
room.simulate(snr=SNR)
room_no_noise_left.simulate(snr=90)
room_no_noise_right.simulate(snr=90)

#畳み込んだ波形を取得する(チャンネル、サンプル）
multi_conv_data=room.mic_array.signals
multi_conv_data_left_no_noise=room_no_noise_left.mic_array.signals
multi_conv_data_right_no_noise=room_no_noise_right.mic_array.signals

#畳み込んだ波形をファイルに書き込む
write_file_from_time_signal(multi_conv_data_left_no_noise[0,:]*np.iinfo(np.int16).max/20.,"./ica_left_clean.wav",sample_rate)

#畳み込んだ波形をファイルに書き込む
write_file_from_time_signal(multi_conv_data_right_no_noise[0,:]*np.iinfo(np.int16).max/20.,"./ica_right_clean.wav",sample_rate)

#畳み込んだ波形をファイルに書き込む
write_file_from_time_signal(multi_conv_data[0,:]*np.iinfo(np.int16).max/20.,"./ica_in_left.wav",sample_rate)
write_file_from_time_signal(multi_conv_data[0,:]*np.iinfo(np.int16).max/20.,"./ica_in_right.wav",sample_rate)

#短時間フーリエ変換を行う
f,t,stft_data=sp.stft(multi_conv_data,fs=sample_rate,window="hann",nperseg=N)

#ICAの繰り返し回数
n_ica_iterations=5

#ILRMAの基底数
n_basis=2

#処理するフレーm数
Lt=np.shape(stft_data)[-1]


#EMアルゴリズムに基づくLGM実行コード
Rlgm_em,vlgm_em,y_lgm_em,cost_buff_lgm_em=execute_em_lgm(stft_data,Ns=n_sources,n_iterations=n_ica_iterations)
permutation_index_result=solver_inter_frequency_permutation(y_lgm_em[0,...])
#パーミュテーションを解く
for k in range(Nk):
    y_lgm_em[:,:,k,:]=y_lgm_em[:,permutation_index_result[k],k,:]

lgm_em_time=time.time()

t,y_lgm_em=sp.istft(y_lgm_em[0,...],fs=sample_rate,window="hann",nperseg=N)


snr_pre=calculate_snr(multi_conv_data_left_no_noise[0,...],multi_conv_data[0,...])+calculate_snr(multi_conv_data_right_no_noise[0,...],multi_conv_data[0,...])
snr_pre/=2.

snr_lgm_em_post1=calculate_snr(multi_conv_data_left_no_noise[0,...],y_lgm_em[0,...])+calculate_snr(multi_conv_data_right_no_noise[0,...],y_lgm_em[1,...])
snr_lgm_em_post2=calculate_snr(multi_conv_data_left_no_noise[0,...],y_lgm_em[1,...])+calculate_snr(multi_conv_data_right_no_noise[0,...],y_lgm_em[0,...])

snr_lgm_em_post=np.maximum(snr_lgm_em_post1,snr_lgm_em_post2)
snr_lgm_em_post/=2.


write_file_from_time_signal(y_lgm_em[0,...]*np.iinfo(np.int16).max/20.,"./lgm_em_1.wav",sample_rate)
write_file_from_time_signal(y_lgm_em[1,...]*np.iinfo(np.int16).max/20.,"./lgm_em_2.wav",sample_rate)





