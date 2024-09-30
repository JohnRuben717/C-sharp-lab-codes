using System;
using System.Collections.Generic;

class InfixToPostfixConverter
{
    static int Precedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            case '^':
                return 3;
            default:
                return 0;
        }
    }

    static bool IsOperator(char ch)
    {
        return ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^';
    }

    static bool IsOperand(char ch)
    {
        return char.IsLetterOrDigit(ch);
    }

    public static string InfixToPostfix(string infix)
    {
        Stack<char> stack = new Stack<char>();
        string postfix = "";

        foreach (char ch in infix)
        {
            if (IsOperand(ch))
            {
                postfix += ch;
            }
            else if (ch == '(')
            {
                stack.Push(ch);
            }
            else if (ch == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    postfix += stack.Pop();
                }
                stack.Pop();
            }
            else if (IsOperator(ch))
            {
                while (stack.Count > 0 && Precedence(ch) <= Precedence(stack.Peek()))
                {
                    postfix += stack.Pop();
                }
                stack.Push(ch);
            }
        }

        while (stack.Count > 0)
        {
            postfix += stack.Pop();
        }

        return postfix;
    }

    static void Main()
    {
        Console.Write("Enter infix expression: ");
        string infix = Console.ReadLine();
        string postfix = InfixToPostfix(infix);
        Console.WriteLine($"Postfix expression: {postfix}");
    }
}
