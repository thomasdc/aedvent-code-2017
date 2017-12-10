package main

import (
	"fmt"
	"math"
)

type Direction int

type Coord struct {
	X int
	Y int
}

const ( // iota is reset to 0
	up    Direction = iota
	down  Direction = iota
	right Direction = iota
	left  Direction = iota
)

func main() {
	v := calculateBiggerThan(368078)
	fmt.Println(v)
}

func calculateBiggerThan(n int) int {
	m := make(map[Coord]int)
	// not the most efficient but we can reuse a function.
	for i := 1; i < 10000; i++ {
		x, y := calculatePosition(i)
		v := 1
		if i != 1 {
			v = calculateFromNeighbours(x, y, m)
			if v > n {
				return v
			}
		}
		m[Coord{x, y}] = v
		fmt.Println("i:", i, "x-y", x, y)
	}
	return -1
}

func calculateFromNeighbours(x int, y int, m map[Coord]int) int {
	relNeighbourCoords := [8]Coord{Coord{1, 0}, Coord{1, 1}, Coord{0, 1}, Coord{-1, 1}, Coord{-1, 0}, Coord{-1, -1}, Coord{0, -1}, Coord{1, -1}}

	sum := 0
	for _, rnc := range relNeighbourCoords {
		if val, ok := m[Coord{rnc.X + x, rnc.Y + y}]; ok {
			sum = sum + val
		}
	}

	fmt.Println(sum)
	return sum
}

func addAbsolute(n1 int, n2 int) int {
	return int(math.Abs(float64(n1)) + math.Abs(float64(n2)))
}

func calculatePosition(number int) (int, int) {
	_, previousMax, sideLength := calculateStepsSideLengthPreviousMax(number)

	maxDistanceFromZero := ((sideLength - 1) / 2)
	sx, sy := calculateStartPosition(sideLength)
	direction := up

	if previousMax == 0 {
		sx = 0
		sy = 0
		direction = right
	}
	// loop laatste cirkel af
	for i := previousMax + 1; i < number; i++ {
		if direction == up {
			if sy >= maxDistanceFromZero {
				direction = nextDirection(direction)
			}
			sx, sy = moveForward(sx, sy, direction)

		} else if direction == left {
			if sx <= -maxDistanceFromZero {
				direction = nextDirection(direction)
			}
			sx, sy = moveForward(sx, sy, direction)

		} else if direction == down {
			if sy <= -maxDistanceFromZero {
				direction = nextDirection(direction)
			}
			sx, sy = moveForward(sx, sy, direction)

		} else if direction == right {
			if sx >= maxDistanceFromZero {
				direction = nextDirection(direction)
			}
			sx, sy = moveForward(sx, sy, direction)
		}
	}
	return sx, sy
}

func moveForward(x int, y int, dir Direction) (int, int) {
	switch dir {
	case up:
		return x, y + 1
	case left:
		return x - 1, y
	case down:
		return x, y - 1
	case right:
		return x + 1, y
	default:
		panic("PANIEK!")
	}
}

func nextDirection(dir Direction) Direction {

	switch dir {
	case up:
		//		fmt.Println("switching up->left")
		return left
	case left:
		//		fmt.Println("switching left->down")
		return down
	case down:
		//		fmt.Println("switching down->right")
		return right
	case right:
		return up
	default:
		panic("PANIEK!")
	}
}

func calculateStartPosition(sideLength int) (int, int) {

	x := (sideLength - 1) / 2
	y := -(((sideLength - 1) / 2) - 1)
	return x, y
}

func calculateStepsSideLengthPreviousMax(number int) (int, int, int) {
	someBigValue := 1000000000
	previousMax := 0
	currentSideLength := 1
	for steps := 0; steps < someBigValue; steps++ {
		currentSideLength += 2
		currentMax := currentSideLength * currentSideLength
		if currentMax >= number {
			return steps, previousMax, currentSideLength
		}
		previousMax = currentMax
	}
	panic("increase some big value")
}
