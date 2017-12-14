package main

import (
	"testing"
)

var tests = []struct{
	input string
	output int
}{
	{
		input: "flqrgnkx",
		output: 8108,
	},
}

func TestPart1(t *testing.T){
	
	for _, test := range tests {

		actual := Part1(test.input)
		
		if actual != test.output {
			t.Fatalf("Mistakes were made.. %s returned %d expecting %d.", 
				test.input,
				actual, 
				test.output,
			)
		}
	}
}