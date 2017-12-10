package main.scala.y2017.Day09

object Runner {
  def run(input: String, countGarbage: Boolean = false): Int = {
    var depth, score, garbageCount = 0
    var skipNext, inGarbage = false

    input.toCharArray.foreach {c => {
      if(skipNext) skipNext = false
      else {
        c match {
          case '{' =>
            if(!inGarbage) depth = depth + 1
            else garbageCount = garbageCount + 1

          case '}' =>
            if(!inGarbage) {
              score += depth
              depth -= 1
            }
            else garbageCount = garbageCount + 1

          case '!' => skipNext = true

          case '<' =>
            if(inGarbage) garbageCount = garbageCount + 1
            inGarbage = true

          case '>' => inGarbage = false

          case _ =>
            if(inGarbage) garbageCount = garbageCount + 1
        }
      }
    }}

    if(countGarbage) garbageCount
    else score
  }
}