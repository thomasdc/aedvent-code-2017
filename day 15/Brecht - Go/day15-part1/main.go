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

	aChan := make(chan int, 5000000)
	bChan := make(chan int, 5000000)
	done := make(chan bool)

	fmt.Println("Launching")
	go generateA(aChan, done)
	go generateB(bChan, done)

	go judge(aChan, bChan, done)
	fmt.Println("Waiting for done")

	<-done
	fmt.Println("DONE")

}

func generateA(aChan chan int, done chan bool) {
	aStartingValue := 634
	aFactor := 16807
	divider := 2147483647
	for len(done) == 0 {
		aStartingValue = generateNextValue(aStartingValue, aFactor, divider)
		if aStartingValue%4 == 0 {
			aChan <- aStartingValue
		}
	}
}

func generateB(bChan chan int, done chan bool) {

	bStartingValue := 301
	bFactor := 48271
	divider := 2147483647
	for len(done) == 0 {
		bStartingValue = generateNextValue(bStartingValue, bFactor, divider)
		if bStartingValue%8 == 0 {
			bChan <- bStartingValue
		}
	}
}

func judge(aChan chan int, bChan chan int, done chan bool) {
	sum := 0
	validated := 0

	for validated < 5000000 {

		aValue := <-aChan
		bValue := <-bChan
		aFirst16bits := aValue % 65536
		bFirst16bits := bValue % 65536
		if aFirst16bits == bFirst16bits {
			sum += 1
		}
		validated += 1
		if validated%10000 == 0 {
			fmt.Println("validated", validated, len(aChan), len(bChan))
		}
	}
	fmt.Println(sum)
	done <- true

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
