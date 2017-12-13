package main

import (
	"encoding/csv"
	"fmt"
	"math"
	"os"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	directions := readFile("input.txt")
	x := 0
	y := 0
	fmt.Println("startpos", x, y)
	xEnd, yEnd, max := travelandGetMax(x, y, directions)
	fmt.Println("endpos", xEnd, yEnd)
	fmt.Println("max", max)

}

func travelandGetMax(x int, y int, directions []string) (int, int, int) {
	max := 0
	orgx := x
	orgy := y
	for _, direction := range directions {
		x, y = move(x, y, direction)
		distance := closestDistance(orgx, orgy, x, y)
		if distance > max {
			max = distance
		}

	}
	return x, y, max
}

//x=1|x=2
//    _
//  _/ \    y=3
// / \_/    y=2
// \_/ \
//   \_/    y=1

// Going North east = y + 1, x + 1
// Going South east = y - 1, x + 1
// Going North = y + 2
// Going South = y - 2
// Going North west = y + 1, x - 1
// Going South west = y - 1, x - 1

func move(x int, y int, direction string) (int, int) {
	switch direction {
	case "ne":
		return x + 1, y + 1
	case "se":
		return x + 1, y - 1
	case "n":
		return x, y + 2
	case "s":
		return x, y - 2
	case "nw":
		return x - 1, y + 1
	case "sw":
		return x - 1, y - 1
	default:
		panic("PANIEK ZEG IK U!" + direction)
	}
}
func closestDistance(x1 int, y1 int, x2 int, y2 int) int {
	yDistance := int(math.Abs(float64(y2 - y1)))
	xDistance := int(math.Abs(float64(x2 - x1)))
	fmt.Println(xDistance)
	moveDiagonal := int(math.Min(float64(yDistance), float64(xDistance)))
	fmt.Println("diagonal moves:", moveDiagonal)
	moveUpDown := (int(math.Max(float64(yDistance-moveDiagonal), 0.0))) / 2
	fmt.Println("updown moves:", moveUpDown)
	moveLeftRight := int(math.Max(float64(xDistance-moveDiagonal), 0.0))
	fmt.Println("leftright moves:", moveLeftRight)

	return moveDiagonal + moveUpDown + moveLeftRight

}

func readFile(filePath string) []string {
	file, err := os.Open(filePath)
	check(err)
	r := csv.NewReader(file)
	r.Comma = ','
	records, err := r.ReadAll()
	check(err)
	return records[0]
}
