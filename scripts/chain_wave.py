import wave
import glob
import numpy as np
import pathlib

def chain(wavfiles, start, end):
    outname = "problem";
    for x in range(start, end + 1):
        outname += str(x)
    outname += ".wav"
        
    outwave = []
    for i in range(start, end + 1):
        with wave.open(wavfiles[i], mode="r") as wav:
            wavdata = np.frombuffer(wav.readframes(wav.getnframes()), dtype=np.int16)
            
        outwave += list(wavdata)
        
    with wave.open(outname, mode="w") as wav:
        wav.setnchannels(1)
        wav.setsampwidth(2)
        wav.setframerate(48000)
        wav.writeframes(np.array(outwave, dtype=np.int16))
    
    return str(pathlib.Path(outname).absolute())

waves_files = glob.glob("problem?_*.wav")

waves_files.sort()
key_to_wave = {int(x[7]): x for x in waves_files}
outwaves = []

start = 1
for i in range(1, 100): 
    if not i in key_to_wave:
        start = i + 1
    if i+1 in key_to_wave:
        continue
        
    if start > i:
        start = i + 1
        continue
        
    if start == i:
        start = i + 1
        continue
        
    outwaves.append(chain(key_to_wave, start, i))
    start = i + 1


print(",".join(outwaves), end="")