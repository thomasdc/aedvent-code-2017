package main.scala.y2017.Day02

object Runner extends Runner {}

class Runner {
  type Sheet = Array[Array[Int]]

  def run(sheet: Sheet): Int = sheet.map(row => largestDiff(row)).sum

  def run2(sheet: Sheet): Int = sheet.map(row => specialDiff(row)).sum

  def largestDiff(row: Array[Int]): Int = row.max - row.min

  def specialDiff(row: Array[Int]): Int = {
    row.indices.foreach(x =>
      row.indices.foreach(y =>
        if(row(x) != row(y) && row(x) % row(y) == 0)
          return row(x) / row(y)
      )
    )

    throw new Exception("Aaargh solution should be found!!")
  }
}
