package main

import (
	"testing"
)

var tests = []struct{
	input string
	output int
}{
	{ "{}"                           , 1 },
	{ "{{{}}}"                       , 6 },
	{ "{{},{}}"                      , 5 },
	{ "{{{},{},{{}}}}"               ,16 },
	{ "{<a>,<a>,<a>,<a>}"            , 1 },
	{ "{{<ab>},{<ab>},{<ab>},{<ab>}}", 9 },
	{ "{{<!!>},{<!!>},{<!!>},{<!!>}}", 9 },
	{ "{{<a!>},{<a!>},{<a!>},{<ab>}}", 3 },
}




func TestParseScore(t *testing.T){
	
	for _, test := range tests {

		actual := ParseScore(test.input)
		
		if actual != test.output {
			t.Fatalf("Mistakes were made.. %q returned %d expecting %d.", 
				test.input, 
				actual, 
				test.output,
			)
		}
	}

}