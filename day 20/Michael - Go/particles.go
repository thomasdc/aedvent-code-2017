// You can edit this code!
// Click here and start typing.
package main

import (
    "fmt"
    "math"
    "time"
	"sort"
	"bufio"
	"strings"
	"strconv"
    "os"
)

// Structs and methods
type Vertex struct {
	X int
	Y int
	Z int
}

func (v Vertex) Sum(v1 Vertex) Vertex {
	return Vertex{ v.X + v1.X, v.Y + v1.Y, v.Z + v1.Z }
}

type Particle struct {
	id int
	p Vertex
	v Vertex
	a Vertex
}

func (ptc *Particle) Next() {
	v_next := ptc.v.Sum(ptc.a)
	ptc.p = ptc.p.Sum(v_next)
	ptc.v = v_next
}

func (ptc Particle) Distance() float64 {
	return math.Abs(float64(ptc.p.X)) + math.Abs(float64(ptc.p.Y)) + math.Abs(float64(ptc.p.Z))
}

// Parsing
func parseParticle(id int, line string) Particle {
	parts := strings.Split(line, ", ")
	return Particle{ id, parseVertex(parts[0]), parseVertex(parts[1]), parseVertex(parts[2]) }
}

func parseVertex(line string) Vertex {
	parts := strings.Split(line[3:len(line)-1], ",")
    X, _ := strconv.ParseInt(parts[0], 10, 32)
    Y, _ := strconv.ParseInt(parts[1], 10, 32)
    Z, _ := strconv.ParseInt(parts[2], 10, 32)
	return Vertex{ int(X), int(Y), int(Z) }
}

// Parsing
func main() {	
    file, _ := os.Open("input.txt")
    defer file.Close()
	scanner := bufio.NewScanner(file)
	particles1 := []*Particle{}
	particles2 := []*Particle{}
	id := 0
    for scanner.Scan() {
		text := scanner.Text()
		p1 := parseParticle(id, text)
		p2 := parseParticle(id, text)
		particles1 = append(particles1, &p1)
		particles2 = append(particles2, &p2)
		
		id ++;
    }
	
	start := time.Now()
	run(particles1, 10000, false)
	elapsed := time.Since(start)
	fmt.Println("------ Puzzle 1 took: ", elapsed)

	start = time.Now()
	run(particles2, 10000, true)
	elapsed = time.Since(start)
	fmt.Println("------ Puzzle 2 took: ", elapsed)
}

func run(particles []*Particle, iterations int, collisions bool) {
	i := 0
	if (collisions) {
		particles = removeCollisions(particles)
	}

	for i < iterations {
		cycle(particles)

		if (collisions) {
			particles = removeCollisions(particles)
		}
		i++
	}
	
	sort.Slice(particles[:], func(i, j int) bool {
		return particles[i].Distance() < particles[j].Distance()
	})
	
	fmt.Println("Particles left: ", len(particles))
	fmt.Println("Closest particle: ", particles[0].id)
}

func cycle(particles []*Particle) {
	for _, p := range particles {
		p.Next()
	}
}

func removeCollisions(a []*Particle) []*Particle {
	result := []*Particle{}

	pos := map[Vertex][]int{}
	for _, particle := range a {
		if ids, ok := pos[particle.p]; ok { 
			pos[particle.p] = append(ids, particle.id)
		} else {
			pos[particle.p] = []int{particle.id}
		}
	}
	for _, particle := range a {
		if ids, ok := pos[particle.p]; ok { 
			if len(ids) == 1 {
				result = append(result, particle)
			}
		}
	}
	
	return result 
}