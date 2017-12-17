package main.scala.y2017.Day17

object Runner {
  def run(steps: Int, itemsToInsert: Int = 2017): Int = {
    var position = 0
    val buffer = scala.collection.mutable.ArrayBuffer(0)

    (1 to itemsToInsert).foreach(insert => {
      position = (position + steps) % buffer.length + 1
      buffer.insert(position, insert)
    })

    buffer(buffer.indexOf(itemsToInsert) + 1)
  }

  //  This solution can only work if the point of interest is 0 because no insertions can happen before it
  def run2(steps: Int, itemsToInsert: Int = 50000000): Int = {
    var position, lastValueInsertedAfterPointOfInterest = 0
    var virtualBufferLength = 1

    (1 to itemsToInsert).foreach(insert => {
      position = (position + steps) % virtualBufferLength + 1
      virtualBufferLength += 1
      if(position == 1)
        lastValueInsertedAfterPointOfInterest = insert
    })

    lastValueInsertedAfterPointOfInterest
  }
}
