package main

import (
	"encoding/hex"
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
	//	input := "flqrgnkx"
	input := "amgozmfv"
	matrix := createMatrix(input)
	used := countUsed(matrix)
	groups := countGroupUsed(matrix)
	fmt.Println(used, groups)
}

func countGroupUsed(matrix Slice) int {
	currentGroupId := 2
	for i, row := range matrix {
		for j, _ := range row.(Slice) {
			rowSlice := row.(Slice)
			if matrix[i].(Slice)[j].(int) == 1 {
				group := checkAdjacentGroups(matrix, i, j)
				if group == -1 {
					group = currentGroupId
					currentGroupId++
				}
				rowSlice[j] = Item(group)
			}
		}
	}
	fmt.Println("-------------------------------------")
	printMatrix(matrix)
	return currentGroupId - 2
}

func printMatrix(matrix Slice) {
	for _, row := range matrix {
		for _, el := range row.(Slice) {
			fmt.Printf(LeftPad2Len(strconv.Itoa(el.(int)), " ", 6))
		}
		fmt.Println("|")
	}
}

func LeftPad2Len(s string, padStr string, overallLen int) string {
	var padCountInt int
	padCountInt = 1 + ((overallLen - len(padStr)) / len(padStr))
	var retStr = strings.Repeat(padStr, padCountInt) + s
	return retStr[(len(retStr) - overallLen):]
}

type Coord struct {
	i int
	j int
}

func checkAdjacentGroups(matrix Slice, i int, j int) int {
	indicesToHandle := CreateStack()
	initialCoordinate := Coord{i, j}
	visited := make(map[Coord]bool)
	indicesToHandle.Push(&initialCoordinate)
	visited[initialCoordinate] = true

	for indicesToHandle.Size() > 0 {

		current := indicesToHandle.Pop()
		coord := current.(*Coord)
		left := Coord{coord.i - 1, coord.j}
		right := Coord{coord.i + 1, coord.j}
		up := Coord{coord.i, coord.j - 1}
		down := Coord{coord.i, coord.j + 1}
		if left.i >= 0 {
			group := matrix[left.i].(Slice)[left.j].(int)
			if group > 1 {
				return group
			}
			if group == 1 && !visited[left] {
				fmt.Println("left")
				indicesToHandle.Push(&left)
				visited[left] = true
			}
		}
		if right.i < len(matrix) {
			group := matrix[right.i].(Slice)[right.j].(int)
			if group > 1 {
				return group
			}
			if group == 1 && !visited[right] {
				fmt.Println("right")

				indicesToHandle.Push(&right)
				visited[right] = true

			}
		}
		if up.j >= 0 {
			group := matrix[up.i].(Slice)[up.j].(int)
			if group > 1 {
				return group
			}
			if group == 1 && !visited[up] {
				fmt.Println("up")

				indicesToHandle.Push(&up)
				visited[up] = true

			}
		}
		if down.j < len(matrix[0].(Slice)) {
			group := matrix[down.i].(Slice)[down.j].(int)
			if group > 1 {
				return group
			}
			if group == 1 && !visited[down] {
				indicesToHandle.Push(&down)
				visited[down] = true
			}
		}
	}
	return -1
}

func countUsed(matrix Slice) int {
	sum := 0
	for _, row := range matrix {
		for _, cellvalue := range row.(Slice) {
			sum += cellvalue.(int)
		}
	}
	return sum
}

func createMatrix(input string) Slice {
	matrix := Slice(make([]Item, 0, 10))
	for i := 0; i < 128; i++ {
		hash := getKnotHash(input + "-" + strconv.Itoa(i))
		byteHexStr, err := hex.DecodeString(hash)
		check(err)

		binaryString := ""
		for _, b := range byteHexStr {
			binaryString = binaryString + hex2Bin(b)
		}

		row := Slice(make([]Item, 0, 10))
		for _, c := range binaryString {
			n, err := strconv.Atoi(string(c))
			check(err)
			row.Append(n)
		}
		matrix.Append(row)
		fmt.Println("hash", hash, "byteHexStr", byteHexStr, "binary", binaryString)
	}
	return matrix
}

func hex2Bin(in byte) string {
	var out []byte
	for i := 7; i >= 0; i-- {
		b := (in >> uint(i))
		out = append(out, (b%2)+48)
	}
	return string(out)
}

func getKnotHash(input string) string {
	numbersLength := 256

	lengths := Slice(make([]Item, len(input), len(input)+1))
	for i, n := range input {
		lengths[i] = int(n)
	}
	for _, n := range []int{17, 31, 73, 47, 23} {
		lengths.Append(n)
	}
	numbers := Slice(make([]Item, 0, 10))
	for i := 0; i < numbersLength; i++ {
		numbers.Append(i)
	}

	curPos := 0
	skip := 0
	for i := 0; i < 64; i++ {
		skip, curPos = hash(numbers, lengths, skip, curPos)
	}

	hashStr := ""
	for i := 0; i < len(numbers)/16; i++ {
		sl := numbers[i*16 : min(i*16+16, len(numbers))]
		res := xorNumbers(sliceToIntSlice(sl))
		hexStr := fmt.Sprintf("%x", res)
		if len(hexStr) == 1 {
			hexStr = "0" + hexStr
		}
		hashStr += hexStr
	}
	return hashStr
}

func xorNumbers(numbers []int) int {
	res := 0
	for _, el := range numbers {
		res = res ^ el
	}
	return res
}

func sliceToIntSlice(s Slice) []int {
	intSlice := make([]int, len(s), cap(s))
	for i, item := range s {
		intSlice[i] = item.(int)
	}
	return intSlice
}

func hash(numbers Slice, lengths Slice, skipSize int, currentPosition int) (int, int) {

	for _, l := range lengths {
		reverse(currentPosition, numbers, l.(int))
		currentPosition = (currentPosition + l.(int) + skipSize) % len(numbers)
		skipSize += 1
	}

	return skipSize, currentPosition
}

func reverse(pos int, numbers Slice, l int) {
	current1 := pos
	current2 := (pos + l - 1)

	for current2 > current1 {
		i1 := current1 % len(numbers)
		i2 := current2 % len(numbers)

		smallest := numbers[i1]
		biggest := numbers[i2]
		numbers[i2] = smallest
		numbers[i1] = biggest

		current1++
		current2--
	}
}

func min(a, b int) int {
	if a < b {
		return a
	}
	return b
}

func max(a, b int) int {
	if a > b {
		return a
	}
	return b
}
