import wave
import matplotlib.pyplot as plt
import numpy as np
import scipy.signal as sp
import argparse
import pathlib
import re
import concurrent.futures as cf

def flatten_wave2(sig):
    sigout = np.asarray(sig)
    prev = 0
    for i in range(sigout.shape[0] - 1):
        if sigout[i] * sigout[i+1] < 0:
            if prev == i:
                sigout[i] /= abs(sigout[i])
            else:
                divisor = np.max(np.abs(sigout[prev:i]))
                sigout[prev:i] /= divisor if divisor != 0 else 1
                #sigout[prev:i] /= abs(max(sigout[prev:i], key=lambda v: abs(v)))
            
            prev = i + 1
    return sigout
    
def flatten_wave(sig):
    sigout = np.asarray(sig)
    sigfactor = sig[:-1] * sig[1:]
    prev = 0
    for i in range(sigout.shape[0] - 1):
        if sigfactor[i]:
            if prev == i:
                sigout[i] /= abs(sigout[i])
            else:
                divisor = np.max(np.abs(sigout[prev:i]))
                sigout[prev:i] /= divisor if divisor != 0 else 1
                #sigout[prev:i] /= abs(max(sigout[prev:i], key=lambda v: abs(v)))
            
            prev = i + 1
    return sigout
    
def calc_correlation(wav_data, chunk_path, is_flatten):
    with wave.open(str(chunk_path), mode="rb") as wav:
        test_data = np.frombuffer(wav.readframes(wav.getnframes()), dtype=np.int16) / 32768
        test_data = test_data.astype(float)
    
    if is_flatten:
        test_data = flatten_wave(test_data)
    
    corr = sp.correlate(test_data, wav_data, mode="valid")
    return corr

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("wav_path")
    parser.add_argument("--cut_range", default=None)
    parser.add_argument("--show_wave", action="store_true")
    parser.add_argument("--flatten_wave", action="store_true")
    parser.add_argument("-n", "--n_show", type=int)
    parser.add_argument("--bench", action="store_true")
    parser.add_argument("--jkspeech")

    args = parser.parse_args()
    WAV_PATH = args.wav_path
    if args.cut_range != None:
        token = args.cut_range.split(",")
        CUT_START = int(token[0])
        CUT_END = int(token[1])
    else:
        CUT_START = 0
        CUT_END = None
    SHOW_WAVE = args.show_wave
    FLATTEN_WAVE = args.flatten_wave
    N_SHOW = args.n_show
    BENCH_MODE = args.bench
    JKSPEECH_PATH = args.jkspeech

    if N_SHOW == None:
        N_SHOW = 10

    if JKSPEECH_PATH == None:
        JKSPEECH_PATH = "./JKspeech"

    TESTWAV = pathlib.Path(JKSPEECH_PATH)

    with wave.open(WAV_PATH, mode="rb") as wav:
        if CUT_END == None:
            CUT_END = wav.getnframes()
        wav_data = np.frombuffer(wav.readframes(wav.getnframes()), dtype=np.int16) / 32768
        wav_data = wav_data.astype(float)[CUT_START:CUT_END]

    if FLATTEN_WAVE:
        wav_data = flatten_wave(wav_data)
        
    if SHOW_WAVE:
        plt.plot(wav_data)
        plt.ylim([-1,1])
        plt.show()

    result_corr_max = []


    executor = cf.ProcessPoolExecutor()
    future_to_path = {executor.submit(calc_correlation, wav_data, str(path), FLATTEN_WAVE): path.name for path in TESTWAV.glob("*.wav")}

    for future in cf.as_completed(future_to_path):
        path = future_to_path[future]
        corr = future.result()
        
        if not BENCH_MODE:
            print("%30s %010f at %08d" % (str(path), corr.max(), corr.argmax()))
        result_corr_max.append([corr.max(), corr.argmax(), str(path)])
        
        #showlist = ["J01", "E02", "J03", "E04", "J05"]
        #for id in showlist:
        #    if id in str(wavpath):
        #            plt.plot(corr)
        #            plt.title("Correlation of " + str(wavpath))
        #            plt.show()
        
    result_corr_max.sort(reverse=True, key=lambda v: v[0])

    if not BENCH_MODE:
        print("\nResult")
        for res in result_corr_max[0:N_SHOW]:
            print("%30s %010f at %08d" % (res[2], res[0], res[1]))
    else:
        ids = [re.search(r"[EJ](0[1-9]|[1-3][0-9]|4[0-4])", res[2]).group(0) for res in result_corr_max[0:N_SHOW]]
        print(" ".join(ids))
    
if __name__ == "__main__":
    main()