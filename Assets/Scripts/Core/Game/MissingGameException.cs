using System;

public class MissingGameException : InvalidOperationException
{
    public MissingGameException() : base("The game does not exist!") { }
}