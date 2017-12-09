package main.scala.y2017.Day09

object Runner extends Runner {}

class Runner {
  def run(input: String, countGarbage: Boolean = false): Int = {
    var depth = 0
    var score = 0
    var skipNext = false
    var inGarbage = false
    var garbageCount = 0

    input.toCharArray.foreach {c => {
      if(skipNext)
        skipNext = false
      else {
        c match {
          case '{' =>
            if(!inGarbage)
              depth = depth + 1
            else
              garbageCount = garbageCount + 1
          case '}' =>
            if(!inGarbage) {
              score = score + depth
              depth = depth - 1
            }
            else
              garbageCount = garbageCount + 1
          case '!' =>
            skipNext = true
          case '<' =>
            if(inGarbage)
              garbageCount = garbageCount + 1
            inGarbage = true
          case '>' =>
            inGarbage = false
          case _ =>
            if(inGarbage)
              garbageCount = garbageCount + 1
        }
      }
    }}

    if(countGarbage)
      garbageCount
    else
      score
  }
}
