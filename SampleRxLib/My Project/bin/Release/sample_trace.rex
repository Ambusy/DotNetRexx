/* sample for interactive debug */
/* at halt enter: */
/* BREAK 10       */
/* OFF            */
/* at next halt:  */
/* CALL P(999)    */
/* press ENTER until end of program */
trace ?n
say 'in s2'
exit
p:
parse arg x
say "in p" x
pull x
return 