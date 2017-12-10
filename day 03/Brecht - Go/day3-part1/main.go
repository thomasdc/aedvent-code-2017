package main

import (
	"fmt"
	"math"
)

type Direction int

const ( // iota is reset to 0
	up    Direction = iota
	down  Direction = iota
	right Direction = iota
	left  Direction = iota
)

func main() {
	// Oh my god I should just have bruteforced this! o_O
	// But the dragon below is performant! :P

	fmt.Println("----------test 1--------------")

	steps, previousMax, sideLength := calculateStepsSideLengthPreviousMax(1)
	fmt.Println("for 1: ", steps, previousMax, sideLength)

	steps, previousMax, sideLength = calculateStepsSideLengthPreviousMax(12)
	fmt.Println("for 12: ", steps, previousMax, sideLength)

	steps, previousMax, sideLength = calculateStepsSideLengthPreviousMax(23)
	fmt.Println("for 23: ", steps, previousMax, sideLength)

	steps, previousMax, sideLength = calculateStepsSideLengthPreviousMax(23)
	fmt.Println("for 35: ", steps, previousMax, sideLength)

	steps, previousMax, sideLength = calculateStepsSideLengthPreviousMax(1024)
	fmt.Println("for 1024: ", steps, previousMax, sideLength)

	fmt.Println("-----------test 2-------------")

	x, y := calculatePosition(1)
	fmt.Println("for 1: ", x, y, addAbsolute(x, y))

	x, y = calculatePosition(12)
	fmt.Println("for 12: ", x, y, addAbsolute(x, y))

	x, y = calculatePosition(23)
	fmt.Println("for 23: ", x, y, addAbsolute(x, y))

	x, y = calculatePosition(35)
	fmt.Println("for 35: ", x, y, addAbsolute(x, y))

	x, y = calculatePosition(1024)
	fmt.Println("for 1024: ", x, y, addAbsolute(x, y))

	x, y = calculatePosition(368078)
	fmt.Println("for 368078: ", x, y, addAbsolute(x, y))

	x, y = calculatePosition(1091027312837901872)
	fmt.Println("for 1091027312837901872: ", x, y, addAbsolute(x, y))
}

func addAbsolute(n1 int, n2 int) int {
	return int(math.Abs(float64(n1)) + math.Abs(float64(n2)))
}

func calculatePosition(number int) (int, int) {
	_, previousMax, sideLength := calculateStepsSideLengthPreviousMax(number)

	maxDistanceFromZero := ((sideLength - 1) / 2)
	sx, sy := calculateStartPosition(sideLength)

	direction := up
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
				fmt.Println("last number?")
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
		panic("IMPOSSIBRU!")
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
		if currentMax > number {
			return steps, previousMax, currentSideLength
		}
		previousMax = currentMax
	}
	panic("increase some big value")
}
