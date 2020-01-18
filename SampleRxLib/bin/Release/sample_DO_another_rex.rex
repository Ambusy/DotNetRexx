/* sample showing how to activate a 2nd rexx using the "DO" part in the DoCmd event handler */
trace n
say 'in s1'
address "DO" "S2"
say 'return in s1'
exit