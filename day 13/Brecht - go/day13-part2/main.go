package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strconv"
	"strings"
	"unicode"
)

type Layer struct {
	depth      int
	scanRange  int
	currentPos int
	op         func(int) int
}

type Player struct {
	depth int
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func main() {
	layerMap, layers := readAll("input.txt")

	fmt.Println(findDelay(&layerMap, layers))

}

func findDelay(layerMap *map[int]*Layer, layers int) int {
	for delay := 0; delay < 10000000; delay++ {
		if !checkCaught(layerMap, layers, delay) {
			return delay
		}
	}
	return -1
}

func checkCaught(layerMapRef *map[int]*Layer, layers int, delay int) bool {
	player := Player{0}
	for player.depth = 0; player.depth <= layers; movePlayer(&player) {
		layerMap := *layerMapRef
		if layerMap[player.depth] != nil {
			c, _ := checkCaughtFirewall(&player, layerMap[player.depth], delay+player.depth)
			if c {
				return true
			}
		}
	}
	return false

}

func getFastCameraPosition(scanRange int, delay int) int {
	rest := delay % ((scanRange - 1) * 2)

	if rest <= scanRange-1 {
		return rest
	} else {
		return (scanRange - 1) - (rest - (scanRange - 1))
	}
}

func checkCaughtFirewall(p *Player, layer *Layer, steps int) (bool, int) {
	pos := getFastCameraPosition(layer.scanRange, steps)
	if pos == 0 {
		return true, layer.depth * layer.scanRange
	} else {
		return false, -1
	}
}

func moveCameras(layerMap *map[int]*Layer) {

	for _, l := range *layerMap {
		l.currentPos = l.op(l.currentPos)
		if l.currentPos == l.scanRange-1 {
			l.op = dec
		}
		if l.currentPos == 0 {
			l.op = add
		}
	}
}

func movePlayer(p *Player) {
	p.depth = p.depth + 1
}

func add(x int) int {
	return x + 1
}

func dec(x int) int {
	return x - 1
}

func readAll(path string) (map[int]*Layer, int) {
	m := make(map[int]*Layer)
	file, err := os.Open(path)
	check(err)
	defer file.Close()
	scanner := bufio.NewScanner(file)
	max := 0
	for scanner.Scan() {
		t := scanner.Text()
		if len(t) > 0 {
			depth, layer := parse(t)
			m[depth] = layer
			if depth > max {
				max = depth
			}
		}
	}

	return m, max
}

func parse(s string) (int, *Layer) {
	re1, err := regexp.Compile(`([\d]+): ([\d]+)`)
	check(err)
	r := re1.FindStringSubmatch(s)
	depthStr := r[1]
	scanRangeStr := r[2]
	depth, err := strconv.Atoi(stripWhitespace(depthStr))
	check(err)
	scanRange, err := strconv.Atoi(stripWhitespace(scanRangeStr))
	check(err)
	l := Layer{depth, scanRange, -1, add}
	return depth, &l
}

func stripWhitespace(s string) string {
	return strings.Map(func(r rune) rune {
		if unicode.IsSpace(r) {
			return -1
		}
		return r
	}, s)
}
