package main

import (
	"testing"
)

var tests = []struct{
	size int
	inputlengths []int
	score int
}{
	{
		size: 5,
		inputlengths: []int{3,4,1,5},
		score: 12,
	},
}

func TestParseScore(t *testing.T){
	
	for _, test := range tests {

		actual := Hash(test.size, test.inputlengths)
		
		if actual != test.score {
			t.Fatalf("Mistakes were made.. %d returned %d expecting %d.", 
				test.inputlengths,
				actual, 
				test.score,
			)
		}
	}
}
