package main

import (
	"time"
	"fmt"
)

func main() {
	arr, last := run(9, 3)
	fmt.Println("Example: ", arr[(last + 1) % len(arr)])

	arr, last = run(2017, 3)
	fmt.Println("Example 2017: ", arr[(last + 1) % len(arr)])

    start := time.Now()
	arr, last = run(2017, 328)
    elapsed := time.Since(start)
	fmt.Println("Puzzle: ", arr[(last + 1) % len(arr)])
    fmt.Println("Puzzle took: ", elapsed)
	
    start = time.Now()
	result := run_track_source(50000000, 328)
    elapsed = time.Since(start)
	fmt.Println("Puzzle 50000000: ", result)
    fmt.Println("Puzzle 50000000 took: ", elapsed)
}

func run(iterations, jmp int) ([]int, int) {
	arr := []int{0}
	currentPos := 0

	for n := 1; n <= iterations; n++ {
		if (n % 10000 == 0) {
			fmt.Println(n, "..")			
		}

		currentPos = (currentPos + jmp) % len(arr) + 1
		arr = append(arr[:currentPos], append([]int{n}, arr[currentPos:]...)...)		
	}

	return arr, currentPos
}

func run_track_source(iterations, jmp int) int {
	arrLen := 1
	currentPos := 0
	targetPos := 0
	result := 0

	for n := 1; n <= iterations; n++ {
		currentPos = (currentPos + jmp) % arrLen + 1
		if (targetPos + 1 % arrLen == currentPos) {
			result = n
		}
		arrLen = arrLen + 1
	}

	return result
}