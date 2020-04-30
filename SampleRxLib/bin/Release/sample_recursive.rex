/* */
trace i
parse arg x, x1
say "in recursive: " x  "|" x1
if x = "" then x = 1
if x>5 then return 99
i = sample_recursive(x+1, "abc")
say "after" i
return x