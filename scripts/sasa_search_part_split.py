import wave
#import matplotlib.pyplot as plt
import numpy as np
import scipy.signal as sp
import argparse
import pathlib
import re
import concurrent.futures as cf
import os
    
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
    
def calc_correlation(wav_data, chunk_path, is_flatten, chunk_size, shift_length=None):
    with wave.open(str(chunk_path), mode="rb") as wav:
        test_data = np.frombuffer(wav.readframes(wav.getnframes()), dtype=np.int16) / 32768
        test_data = test_data.astype(float) 
        
        if chunk_size == None:
            chunk_size = test_data.shape[0]
    
    if is_flatten:
        test_data = flatten_wave(test_data)
    
    if shift_length == None:
        shift_length = chunk_size // 2
    
    correlate_max = []
    
    for chunk_start in range(0, test_data.shape[0] - chunk_size + 1, shift_length):
        test_data_chunk = test_data[chunk_start:chunk_start+chunk_size]
        corr = sp.correlate(test_data_chunk, wav_data, mode="valid")
        correlate_max.append({
            "value": corr.max(),
            "yomipos": chunk_start,
            "targetpos": corr.argmax()
        })
    
    return max(correlate_max, key=lambda x: x["value"])

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("wav_path")
    parser.add_argument("--cut_range", default=None)
    parser.add_argument("--show_wave", action="store_true")
    parser.add_argument("--flatten_wave", action="store_true")
    parser.add_argument("-n", "--n_show", type=int)
    parser.add_argument("--bench", action="store_true")
    parser.add_argument("--jkspeech")
    parser.add_argument("--chunksize", type=int)
    parser.add_argument("--shiftlen", type=int)

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
    CHUNK_SIZE = args.chunksize
    SHIFT_LENGTH = args.shiftlen

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
        
    #calc_correlation(wav_data, "./JKspeech/J01.wav", False, 1024)

    if FLATTEN_WAVE:
        wav_data = flatten_wave(wav_data)
        
    if SHOW_WAVE:
        plt.plot(wav_data)
        plt.ylim([-1,1])
        plt.show()

    result_corr_max = []

    executor = cf.ProcessPoolExecutor(max_workers=os.cpu_count()-4)
    future_to_path = {executor.submit(calc_correlation, wav_data, str(path), FLATTEN_WAVE, CHUNK_SIZE, SHIFT_LENGTH): path.name for path in TESTWAV.glob("*.wav")}

    for future in cf.as_completed(future_to_path):
        path = future_to_path[future]
        corr = future.result()
        
        #if not BENCH_MODE:
        #    print("%30s %010f at (chunk:%08d, target:%08d)" % (path, corr["value"], corr["yomipos"], corr["targetpos"]))
        result_corr_max.append({
            "value": corr["value"],
            "yomipos": corr["yomipos"],
            "targetpos": corr["targetpos"],
            "path": path
        })
        
        #showlist = ["J01", "E02", "J03", "E04", "J05"]
        #for id in showlist:
        #    if id in str(wavpath):
        #            plt.plot(corr)
        #            plt.title("Correlation of " + str(wavpath))
        #            plt.show()
        
    result_corr_max.sort(reverse=True, key=lambda v: v["value"])

    if not BENCH_MODE:
        #print("\nResult")
        for res in result_corr_max[0:N_SHOW]:
            #print("%30s %010f at (chunk:%08d, target:%08d)" % (res["path"], res["value"], res["yomipos"], res["targetpos"]),end=" ")
            print(res["path"], res["value"] / CHUNK_SIZE, CHUNK_SIZE, res["yomipos"], res["targetpos"])
    else:
        ids = [re.search(r"[EJ](0[1-9]|[1-3][0-9]|4[0-4])", res[2]).group(0) for res in result_corr_max[0:N_SHOW]]
        print(" ".join(ids))
    
if __name__ == "__main__":
    main()
