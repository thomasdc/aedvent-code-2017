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
	player := Player{0}
	sum := 0
	for player.depth = 0; player.depth <= layers; movePlayer(&player) {
		moveCameras(&layerMap)
		_, severity := checkCaught(&player, &layerMap)
		sum += severity
	}
	fmt.Println(sum)
}

func checkCaught(p *Player, layerMapPtr *map[int]*Layer) (bool, int) {
	layerMap := *layerMapPtr
	if layerMap[p.depth] != nil && layerMap[p.depth].currentPos == 0 {
		layer := layerMap[p.depth]
		return true, layer.depth * layer.scanRange
	} else {
		return false, 0
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
