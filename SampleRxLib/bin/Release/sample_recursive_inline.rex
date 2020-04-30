/* */
trace n
x = "AAA"
y = "BBBB"
say facul(10) 
say x y
exit 
  
facul:procedure 
parse arg x
say "in facul:" x y
xx="XXX" x
if x > 1 then y = x * facul(x-1)
else y = x
say "return:" x y  xx
return y