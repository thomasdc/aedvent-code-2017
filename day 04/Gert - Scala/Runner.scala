package main.scala.y2017.Day04

object Runner extends Runner {}

class Runner {
  def run(input: String): Boolean = {
    input.split(" ")
      .groupBy(word => word)
      .forall(_._2.length == 1)
  }

  def run(input: Iterator[String]): Int = {
    input.map(line => run(line)).count(valid => valid)
  }

  def run2(input: Iterator[String]): Int = {
    input.map(line => run2(line)).count(valid => valid)
  }

  def run2(input: String): Boolean = {
    val allWords = input.split(" ").zipWithIndex
    allWords.forall(word => {
      val allOtherWords = allWords.filterNot(_._2 == word._2)
      isValid(word._1, allOtherWords.map(_._1))
    })
  }

  def isValid(word: String, otherWords: Array[String]): Boolean = {
    otherWords.forall(otherWord => !isAnagram(word, otherWord))
  }

  def isAnagram(a: String, b: String): Boolean = {
    a.toCharArray.sorted sameElements b.toCharArray.sorted
  }
}
