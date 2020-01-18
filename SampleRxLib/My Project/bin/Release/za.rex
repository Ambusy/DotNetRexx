/* */
pull n
trace r
say expr(n) 
exit 
expr: procedure  
 parse arg e 
 if e > 1
 then return fac(e) + expr(e-1)
 else return fac(e)
fac: procedure 
 parse arg f 
 if f > 1 
 then return f * fac(f-1)
 else return f 