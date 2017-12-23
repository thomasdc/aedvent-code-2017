package main.scala.y2017.Day21
import org.scalatest.{Matchers, FunSuite}
import scala.io.Source

object Runner {
  type Grid = Array[Array[Char]]
  type Rule = (Grid, Grid)
  type Rule2 = (String, Grid)
  type Rules = Map[String, Grid]

  def run(input: Array[String], iterations: Int): Int = {
    val rules = addDerivatives(input.map(parse))
      .map(rule => asString(rule._1) -> rule._2).toMap

    on(Stream.iterate(initialGrid()){ grid => iterate(grid, rules) }(iterations))
  }

  def iterate(grid: Grid, rules: Rules): Grid = {
    val size = if(grid.length % 2 == 0) 2 else 3

    grid.grouped(size).toArray.par.map(rowSlice => {
      rowSlice.transpose.grouped(size).toArray.par
        .map(subGrid => rules.get(asString(subGrid)).get) // Evolve
        .reduce((a, b) => a ++ b).transpose // Merge
    }).toArray.flatten
  }

  def initialGrid(): Grid =
    Array(".#.".toCharArray, "..#".toCharArray, "###".toCharArray)

  def on(grid: Grid): Int =
    grid.map(row => row.count(on => on == '#')).sum

  def addDerivatives(rules: Array[Rule]): Array[Rule] =
    rules.flatMap(rule => addDerivatives(rule))

  def addDerivatives(rule: Rule): Array[Rule] = rotations(rule).flatMap(flips)

  def rotations(rule: Rule): Array[Rule] = {
    val rotation1 = transposed(rule._1)
    val rotation2 = transposed(rotation1)
    val rotation3 = transposed(rotation2)

    Array(rule,
      (rotation1, rule._2),
      (rotation2, rule._2),
      (rotation3, rule._2))
  }

  def transposed(grid: Grid): Grid = {
    grid.indices.map(i =>
      grid(i).indices.map(j => grid(grid(i).length - 1 - j)(i)).toArray
    ).toArray
  }

  def flips(rule: Rule): Array[Rule] = {
    // Swap first and last row to create a flip
    val newRule = (rule._1.clone(), rule._2.clone())

    val firstRow = newRule._1(0)
    newRule._1.update(0, newRule._1(newRule._1.length - 1))
    newRule._1.update(newRule._1.length - 1, firstRow)

    Array(rule, newRule)
  }

  def asString(grid: Grid): String =
    grid.map(_.mkString("")).mkString("/")

  def parse(input: String): Rule = {
    val parts = input.split(" => ")
    val from = parts.head.split("/").map(_.toCharArray)
    val to = parts(1).split("/").map(_.toCharArray)
    (from, to)
  }
}
