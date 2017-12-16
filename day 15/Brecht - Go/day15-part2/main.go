package main

import (
	"fmt"
	"strconv"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	aStartingValue := 634
	bStartingValue := 301

	aFactor := 16807
	bFactor := 48271
	divider := 2147483647

	sum := 0

	for i := 0; i < 40000000; i++ {
		aStartingValue = generateNextValue(aStartingValue, aFactor, divider)
		bStartingValue = generateNextValue(bStartingValue, bFactor, divider)
		aFirst16bits := aStartingValue % 65536
		bFirst16bits := bStartingValue % 65536
		if aFirst16bits == bFirst16bits {
			sum += 1
		}
	}
	fmt.Println(sum)

}

func generateNextValue(startVal int, factor int, divider int) int {
	return (startVal * factor) % divider
}

func asBinary(n int) string {
	return LeftPad2Len(strconv.FormatInt(int64(n), 2), "0", 32)
}

// LeftPad2Len https://github.com/DaddyOh/golang-samples/blob/master/pad.go
func LeftPad2Len(s string, padStr string, overallLen int) string {
	var padCountInt int
	padCountInt = 1 + ((overallLen - len(padStr)) / len(padStr))
	var retStr = strings.Repeat(padStr, padCountInt) + s
	return retStr[(len(retStr) - overallLen):]
}
