/* read format descriptor of a wav file */ 
trace n 
say "WAV filenaam"
pull filename 
if right(filename,4) <> ".WAV" then filename = filename || ".wav"
filename = Stream(filename,"C", "QUERY EXIST") 
if filename = "" then do 
  say "file does not exist" 
  exit 4 
end 
/* in client: winmm.dll/mmioformat    in rexx: mmioFmt   */
call RxFuncAdd("mmioFormat", "winmm.dll","mmioFmt")
format = mmioFmt(FileName) 
parse var format wFormatTag nChannels nSamplesPerSec nAvgBytesPerSec nBlockAlign wBitsPerSample cbSize
say "FormatTag =" wFormatTag
say "Channels =" nChannels
say "SamplesPerSec =" nSamplesPerSec
say "AvgBytesPerSec =" nAvgBytesPerSec
say "BlockAlign =" nBlockAlign
say "BitsPerSample =" wBitsPerSample
say "Size =" cbSize
pull stacked
say "stacked: " stacked
exit