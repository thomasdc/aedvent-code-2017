package main.scala.y2017.Day25

import main.scala.TestBase
class Tests extends TestBase {
  test("Example 1") {
    Runner.run(List(
      ("A", Map(0 -> (1, 1, "B"), 1 -> (-1, 0, "B"))),
      ("B", Map(0 -> (-1, 1, "A"), 1 -> (1, 1, "A")))), "A", 6) shouldBe 3
  }

  test("Puzzle 1") {
    Runner.run(List(
      ("A", Map(0 -> (1, 1, "B"), 1 -> (-1, 1, "E"))),
      ("B", Map(0 -> (1, 1, "C"), 1 -> (1, 1, "F"))),
      ("C", Map(0 -> (-1, 1, "D"), 1 -> (1, 0, "B"))),
      ("D", Map(0 -> (1, 1, "E"), 1 -> (-1, 0, "C"))),
      ("E", Map(0 -> (-1, 1, "A"), 1 -> (1, 0, "D"))),
      ("F", Map(0 -> (1, 1, "A"), 1 -> (1, 1, "C")))), "A", 12523873) shouldBe 4225
  }
}
