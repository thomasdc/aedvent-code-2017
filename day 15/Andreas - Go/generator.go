package main

import "fmt"

func main() {
	a, b := 512, 191 //65, 8921
	iterations := 40000000

	fmt.Println( Part1(a,b,iterations) )

}

func Part1(a,b int, iterations int) int{
	ctr:= 0
	for i := 0 ; i < iterations ; i++{
		a, b = genNext(a,b)
		if a%65536 == b%65536 { ctr++ }
	}
	return ctr
}

func genNext(a, b int) (int,int){
		factA, factB := 16807, 48271
		modulo := 2147483647

		nextA := (a * factA) % modulo
		nextB := (b * factB) % modulo

		return nextA, nextB
}