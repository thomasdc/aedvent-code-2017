package puzzle

import (
	"fmt"
	"io/ioutil"
	"strconv"
	"strings"
	"testing"

	"github.com/stretchr/testify/assert"
)

func ReadFile(path string) string {
	b, err := ioutil.ReadFile(path)
	if err != nil {
		fmt.Print(err)
	}

	return string(b)
}

func ParseNumbers(lines []string) []int {
	parsed := make([]int, len(lines))

	for i, raw := range lines {
		p, _ := strconv.Atoi(raw)
		parsed[i] = p
	}

	return parsed
}

func Run(o Offsetter, offsets []int) int {
	steps := 0
	location := 0
	for {
		steps++
		offset := offsets[location]
		offsets[location] = o.Offset(offset)
		location += offset

		if location < 0 || location >= len(offsets) {
			return steps
		}
	}
}

func PartOne(input []int) int {
	return Run(Part1Offsetter{}, input)
}

func PartTwo(input []int) int {
	return Run(Part2Offsetter{}, input)
}

type Offsetter interface {
	Offset(o int) int
}
type Part1Offsetter struct{}

func (Part1Offsetter) Offset(o int) int {
	return o + 1
}

type Part2Offsetter struct{}

func (Part2Offsetter) Offset(o int) int {
	if o >= 3 {
		return o - 1
	}
	return o + 1
}

func TestPartOneExample(t *testing.T) {
	exampleInput := []int{0, 3, 0, 1, -3}
	assert.Equal(t, 5, PartOne(exampleInput))
}

func TestPartTwoExample(t *testing.T) {
	exampleInput := []int{0, 3, 0, 1, -3}
	assert.Equal(t, 10, PartTwo(exampleInput))
}

func TestCalculateSolutions(t *testing.T) {
	text := ReadFile("input.txt")
	lines := strings.Split(text, "\r\n")
	parsed := ParseNumbers(lines)

	//fmt.Printf("Part 1: %v", PartTwo(parsed))
	fmt.Printf("Part 1: %v", PartTwo(parsed))
	fmt.Println()
}
