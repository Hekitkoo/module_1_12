### The OddEven Kata
	- Write a program that prints numbers within specified range lets say 1 to 100. If number is odd print 'Odd'
	  instead of the number. If number is even print 'Even' instead of number. Else print number [hint - if number is Prime].

#### Steps :

	Lets divide into following steps:
	- Prints numbers from 1 to 100
	- Print "Even" instead of number, if the number is even, means divisible by 2
	- Print "Odd" instead of number, if the number is odd, means not divisible by 2 but not divisible by itself too [hint - it should not be Prime]
	- Print number, if it does not meet above two conditions, means if number is Prime
	- Make method to accept any number of range [currently  we have 1 to 100]
	- Create a new method to check Odd/Even/Prime of a single supplied method

___________________________________________________________________________________________________________________________________________________

#### Calc Stats:

	Your task is to process a sequence of integer numbers
	to determine the following statistics:

		o) minimum value
		o) maximum value
		o) number of elements in the sequence
		o) average value

	For example: [6, 9, 15, -2, 92, 11]

		o) minimum value = -2
		o) maximum value = 92
		o) number of elements in the sequence = 6
		o) average value = 18.166666


__________________________________________________________________________________________________________________________________________________

#### LCD Digits :

	Your task is to create an LCD string representation of an
	integer value using a 3x3 grid of space, underscore, and
	pipe characters for each digit. Each digit is shown below
	(using a dot instead of a space)

	._.   ...   ._.   ._.   ...   ._.   ._.   ._.   ._.   ._.
	|.|   ..|   ._|   ._|   |_|   |_.   |_.   ..|   |_|   |_|
	|_|   ..|   |_.   ._|   ..|   ._|   |_|   ..|   |_|   ..|


	Example: 910

	._. ... ._.
	|_| ..| |.|
	..| ..| |_|