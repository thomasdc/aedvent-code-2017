object Checksum extends App {
  var result = 0
 Util.readAllLines.foreach(addToCheckSum)
  println(result)

  def addToCheckSum(in: String): Unit = {
    val tmpList = in.split("\\s")
    val intList = tmpList.map(x => x.toInt)
    val tmp = intList.max - intList.min
    println(tmp)
    result += tmp
  }
}
