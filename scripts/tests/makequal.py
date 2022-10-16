import pathlib
import argparse
import wave
import numpy as np

class Program:
    WAV_SEARCH_PATH = "./JKspeech"
    
    def main(self):
        parser = argparse.ArgumentParser()
        parser.add_argument("--output", "-o")
        parser.add_argument("wav_list", nargs="*")
        
        args = parser.parse_args()
        OUTPUT_PATH = args.output
        WAVES = Program.parse_wavlist(args.wav_list)
        
        #if OUTPUT_PATH == None:
        #    OUTPUT_PATH
        
        generated_wave = np.array([], dtype=np.int16)
        for wavinfo in WAVES:
            wav_file = wavinfo["file"]
            play_offset = wavinfo["offset"]
            play_start = wavinfo["start"]
            play_end = wavinfo["end"]
            
            with wave.open(self.WAV_SEARCH_PATH + "/" + wav_file + ".wav", mode="rb") as wav:
                wav_data = np.frombuffer(wav.readframes(wav.getnframes()), dtype=np.int16)
            
            wav_data = wav_data[play_start:play_end]
            wav_data = np.pad(wav_data, (play_offset, 0))
            
            if wav_data.shape[0] > generated_wave.shape[0]:
                generated_wave = np.pad(generated_wave, (wav_data.shape[0] - generated_wave.shape[0], 0))
            else:
                wav_data = np.pad(wav_data, (generated_wave.shape[0] - wav_data.shape[0], 0))
            
            generated_wave += wav_data
            
        Program.save_wave(OUTPUT_PATH, generated_wave.astype(np.float64) / 32768, 48000)
        
    def parse_wavlist(raw):
        result = []
        for wavinfo in raw:
            token = wavinfo.split(",")
            result.append({
                "file": token[0],
                "offset": int(token[1]),
                "start": int(token[2]),
                "end": int(token[3])
            })
        
        return result
        
    def save_wave(outpath, wavdata, sr):
        with wave.open(outpath, mode="wb") as wav:
            wav.setnchannels(1)
            wav.setsampwidth(2)
            wav.setframerate(sr)
            wav.writeframesraw((wavdata * 32768).astype(np.int16))
    
    def get_default_outpath(waves):
        result = ""
        for wavinfo in waves:
            wav_file = wavinfo["file"]
            play_offset = wavinfo["offset"]
            play_start = wavinfo["start"]
            play_end = wavinfo["end"]

if __name__ == "__main__":
    Program().main()