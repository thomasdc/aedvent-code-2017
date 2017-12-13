package main

import (
	"testing"
)

var tests = []struct{
	input string
	output int
	groups int
}{
	{
		input: `0: 3
1: 2
4: 4
6: 4`,
		output: 24,
	},
}

func TestPart1(t *testing.T){
	
	for _, test := range tests {

		actual := Severity(test.input)
		
		if actual != test.output {
			t.Fatalf("Mistakes were made.. %s returned %d expecting %d.", 
				test.input,
				actual, 
				test.output,
			)
		}
	}
}