/* RxFuncAdd sample */
trace i
a=1
/* in client: rxsysfn/SysLoadFuncs    in rexx: SysLoadFunc   */
call RxFuncAdd ("SysLoadFuncs","rxsysfn","SysLoadFunc")
do i=1 to 3
   call SysLoadFunc (a, 1, "abc")
   call SysLoadFunc( a, i, "abc")
end
exit