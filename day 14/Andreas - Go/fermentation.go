package main

/*       
       ..---.. 
     .'  _    `. 
 __..'  (o)    : 
`..__          ; 
     `.       / 
       ;      `..---...___ 
     .'                   `~-. .-') 
    .                         ' _.' 
   :                           : 
   \                           ' 
    +                         J 
     `._                   _.' 
        `~--....___...---~' mh 
*/

import (
	"strconv"
	"fmt"
)

func main(){
	fmt.Println( Part1("hfdlxzhv") )

}

func Part1(s string) int{
	res := 0
	for i := 0 ; i < 128 ; i++ {
		bytes := Hash(s + "-" + strconv.Itoa(i))
		res += CountDucks(bytes)
	}

	return res
}


func CountDucks(hash []byte) int{

	res := 0

	for _, bits := range hash {

		if bits & 1 == 1 { res++ }
		if bits & 2 == 2 { res++ }
		if bits & 4 == 4 { res++ }
		if bits & 8 == 8 { res++ }
		
		if bits & 16  == 16  { res++ }
		if bits & 32  == 32  { res++ }
		if bits & 64  == 64  { res++ }
		if bits & 128 == 128 { res++ }
	}

	return res
}

//copied from day 10
func Hash(input string) []byte{
	list := make([]byte, 256)
	for i := 0 ; i < 256; i++ { list[i] = byte(i) }

	inputLengths := handleInput(input)
	currPos, skipSize := 0,0
	for r := 0; r < 64 ; r++{
		_, currPos, skipSize = HashRound(currPos, skipSize, list,inputLengths)
	}

	return denseIt(list)
}
func HashRound(currPos, skipSize int, list, inputLengths []byte) (int,int,int){

	for _, l := range inputLengths {
		reverseSliceInPlace( list, currPos, int(l) )
		
		currPos += int(l) + skipSize
		skipSize++
	}

	return int(list[0]) * int(list[1]), currPos, skipSize
}
func reverseSliceInPlace(slice []byte, start, size int){
	//copy
	r := make([]byte, size)
	for i := 0; i < size ; i++ {
		r[i] = slice[ (start+i)%len(slice) ]
	}

	//reverse
	for b, e := 0, len(r)-1 ; b<e ; b,e = b+1, e -1 {
		r[b], r[e] = r[e], r[b]
	}

	//put back
	for i := 0; i < size; i++ {
		slice[ (start+i)%len(slice) ] = r[i]
	}
}
func handleInput(input string) []byte{
	
	res := make([]byte, len(input), len(input)+5)
	
	for i, c := range input{
		res[i] = byte(c)
	}

	res = append(res, []byte{17, 31, 73, 47, 23}...)
	return res
}
func denseIt(list []byte) []byte{
	
	res := make([]byte, 0, 16)

	for b,e := 0,16 ; e <= len(list) ; b, e = b+16, e+16 {

		xor := list[b]
		for i := b+1 ; i < e ; i++ {
			xor ^= list[i]
		}
		res = append(res, xor)
	}

	return res
}