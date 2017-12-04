object Checksum2 extends App {
  var result = 0
  Util.readAllLines.foreach(addToCheckSum)
  println(result)

  def addToCheckSum(in: String): Unit = {
    val tmpList = in.split("\\s")
    val intList = tmpList.map(x => x.toInt)
    val filterd = intList.filter(x => hasTwoModulo0Deviders(x, intList))

    result += filterd.max / filterd.min
  }

  def hasTwoModulo0Deviders(in: Int, list: Array[Int]): Boolean = {
    var toReturn = false
    list.foreach {
      case `in` =>
      case x if x % in == 0 => toReturn = true
      case x if in % x == 0 => toReturn = true
      case _ =>
    }
    toReturn
  }
}
