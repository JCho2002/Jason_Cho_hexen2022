using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetCollector : MonoBehaviour
{
    private readonly Board _board;
    private Dictionary<CardType, MoveSet> _moveSets = new Dictionary<CardType, MoveSet>();

    public MoveSetCollector(Board board)
    {
        _moveSets.Add(CardType.Teleport, new TeleportCard(board));
        _moveSets.Add(CardType.Swipe, new SwipeCard(board));
        _moveSets.Add(CardType.Line, new LineCard(board));
        _moveSets.Add(CardType.Push, new PushCard(board));

        _board = board;
    }

    public IMoveSet For(CardType type)
    => _moveSets[type];

    internal bool TryGetMoveSet(CardType type, out MoveSet moveSet)
        => _moveSets.TryGetValue(type, out moveSet);
}
