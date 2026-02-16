using System;
using TreeEditor;

public class QuestionNode: ITreeNode
{
    Func<bool> _question;
    ITreeNode _trueNode;
    ITreeNode _falseNode;
    public QuestionNode(Func<bool> Action, ITreeNode TrueNode, ITreeNode FalseNode)
    {
        _question = Action;
        _trueNode = TrueNode;
        _falseNode = FalseNode;
    }
    public void Execute()
    {
        if (_question.Invoke())
        {
            _trueNode.Execute();
        }
        else
            _falseNode.Execute();
    }
}