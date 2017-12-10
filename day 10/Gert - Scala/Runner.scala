package main.scala.y2017.Day10

object Runner {
  def run(inputs: List[Int], listSize: Int = 256): Int = {
    val list = doRounds(inputs, listSize)
    list(0) * list(1)
  }

  def run2(inputs: String, listSize: Int = 256): String = {
    val inputSuffix = List(17, 31, 73, 47, 23)
    val actualInputs = inputs.toCharArray.map(_.toInt).toList ::: inputSuffix

    createHash(doRounds(actualInputs, listSize, 64))
  }

  def createHash(input: Array[Int]): String =
    input.grouped(16).map(_.reduce((a, b) => a ^ b))
      .map(element => "%02x".format(element)).mkString("")

  def doRounds(inputs: List[Int], listSize: Int, rounds: Int = 1): Array[Int] = {
    var list = initList(listSize)
    var position, skip = 0

    (1 to rounds).foreach(round => {
      inputs.foreach(input => {
        list = rotate(list, position, input)
        position = (position + input + skip) % list.length
        skip += 1
      })
    })

    list
  }

  def initList(listSize: Int): Array[Int] = (0 until listSize).toArray

  def rotate(initialList: Array[Int], from: Int, length: Int): Array[Int] = {
    var list = initialList

    (0 until length / 2).foreach(swapPosition => {
      val position = (from + swapPosition) % list.length
      val otherPosition = (from + length - 1 - swapPosition) % list.length
      list = swap(list, position, otherPosition)
    })

    list
  }

  def swap(list: Array[Int], posA: Int, posB: Int): Array[Int] = {
    val tmp = list(posB)
    list.update(posB, list(posA))
    list.update(posA, tmp)
    list
  }
}