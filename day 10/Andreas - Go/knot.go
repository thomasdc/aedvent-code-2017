package main

import "fmt"

func main(){
	input := []int{63,144,180,149,1,255,167,84,125,65,188,0,2,254,229,24}

	fmt.Println( Hash(256, input) )
}

func Hash(size int, inputLengths []int) int{

	list := make([]int, size)
	for i := 0 ; i < size; i++ { list[i] = i }
		
	currPos := 0
	skipSize := 0
	//fmt.Println(currPos, list)
	for _, l := range inputLengths {

		reverseSliceInPlace( list, currPos, l )
		
		currPos += l + skipSize
		skipSize++
		//fmt.Println(currPos, list, skipSize)
	}


	return list[0] * list[1]
}


func reverseSliceInPlace(slice []int, start, size int){

	//copy
	r := make([]int, size)
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