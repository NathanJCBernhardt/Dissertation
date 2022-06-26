import pygame
import chess
import random

screen = pygame.display.set_mode((480, 480))
pygame.display.set_caption("Chess Engine")
screen.fill("WHITE")

WHITE = (255, 255, 255)
RED = (255, 0, 0)

chessboard = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\chessboard.png').convert_alpha()
King = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\whiteKing.png').convert_alpha()
king = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\blackKing.png').convert_alpha()
Knight = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\whiteKnight.png').convert_alpha()
knight = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\blackKnight.png').convert_alpha()
Rook = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\whiteRook.png').convert_alpha()
rook = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\blackRook.png').convert_alpha()
Queen = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\whiteQueen.png').convert_alpha()
queen = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\blackQueen.png').convert_alpha()
Bishop = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\whiteBishop.png').convert_alpha()
bishop = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\blackBishop.png').convert_alpha()
Pawn = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\whitePawn.png').convert_alpha()
pawn = pygame.image.load(r'C:\Users\Nathan\Desktop\needed\icons\blackPawn.png').convert_alpha()



def draw(fen, col, rank, board) :
    if fen == 'K' :
        if board.turn == chess.WHITE :
            if board.is_check() :
                pygame.draw.circle(screen, RED, (rank + 30, col + 30), 30)
        screen.blit(King, (rank, col))
    elif fen == 'k' :
        if board.turn == chess.BLACK :
            if board.is_check() :
                pygame.draw.circle(screen, RED, (rank + 30, col + 30), 30)
        screen.blit(king, (rank, col))
    elif fen == 'Q' :
        screen.blit(Queen, (rank, col))
    elif fen == 'q' :
        screen.blit(queen, (rank, col))
    elif fen == 'R' :
        screen.blit(Rook, (rank, col))
    elif fen == 'r' :
        screen.blit(rook, (rank, col))
    elif fen == 'N' :
        screen.blit(Knight, (rank, col))
    elif fen == 'n' :
        screen.blit(knight, (rank, col))
    elif fen == 'B' :
        screen.blit(Bishop, (rank, col))
    elif fen == 'b' :
        screen.blit(bishop, (rank, col))
    elif fen == 'P' :
        screen.blit(Pawn, (rank, col))
    elif fen == 'p' :
        screen.blit(pawn, (rank, col))

def show(FEN, board) :
        screen.blit(chessboard, (0, 0))
        col = 0
        rank = 0
        for fen in FEN :
            if fen == '/' :
                col = col + 1
                rank = 0
            elif fen in ('1', '2', '3', '4', '5', '6', '7', '8') :
                rank = rank + int(fen)
            elif fen in ('K', 'k', 'Q', 'q', 'R', 'r', 'N', 'n', 'B', 'b', 'P', 'p') :
                draw(fen, col*60, rank*60, board)
                rank = rank + 1
        pygame.display.update() 

infinity = 1000000000

def piece_list(pos) :
    '''Renvoie la liste des pièces présentes sur l'échiquier.'''
    piece_liste = []
    for place in chess.SQUARES :
        if pos.piece_at(place) != None :
            piece_liste.append(pos.piece_at(place).symbol())
    return piece_liste

def game_is_finished(pos) :
    '''Détermine si la partie est finie.'''
    if pos.is_checkmate() or pos.is_stalemate() or pos.is_insufficient_material() or pos.can_claim_threefold_repetition():
        return True
    else :
        return False

def evaluate(pos) :
    
    '''Évaluation d'une position en fonction d'une position et de la mobillité des pièces.'''
    if pos.is_checkmate() :
        return -infinity
    elif game_is_finished(pos) :
        return 0
    piece_liste = piece_list(pos)
    evaluation = 0
    opponent_legal_moves = []
    current_player_legal_moves = [move for move in pos.legal_moves]
    for i in piece_liste :
        if i == 'Q' :
            evaluation = evaluation + 9
        elif i == 'q' :
            evaluation = evaluation - 9
        elif i == 'R' :
            evaluation = evaluation + 5
        elif i == 'r' :
            evaluation = evaluation - 5
        elif i == 'N' :
            evaluation = evaluation + 3
        elif i == 'n' :
            evaluation = evaluation - 3
        elif i == 'B' :
            evaluation = evaluation + 3
        elif i == 'b' :
            evaluation = evaluation - 3
        elif i == 'P' :
            evaluation = evaluation + 1
        elif i == 'p' :
            evaluation = evaluation - 1

    for i in range(len(current_player_legal_moves)-1) :
        pos.push_san(str(current_player_legal_moves[i]))
        opponent_legal_moves.append(pos.legal_moves.count())
        if pos.is_checkmate() :
            pos.pop()
            return infinity
        elif game_is_finished(pos) :
            pos.pop()
            return 0
        pos.pop()
    moyenne = 0
    for i in opponent_legal_moves :
        moyenne = moyenne + i
    if len(opponent_legal_moves) != 0 :
        moyenne = moyenne / len(opponent_legal_moves)
    #else :   a servi pour le déboggage
        #print(moyenne)
    if pos.turn == chess.WHITE :
        evaluation = evaluation + 0.1*(pos.legal_moves.count() - moyenne)
    else :
        evaluation = evaluation - 0.1*(pos.legal_moves.count() - moyenne)

    return evaluation

def evaluation_turn(pos) :
    '''Évaluation d'une position selon le côté qui doit jouer.'''
    if pos.turn == chess.WHITE :
        return evaluate(pos)
    else :
        return - evaluate(pos)

def one_legal(pos) :
    current_player_legal_moves = [str(move) for move in pos.legal_moves]
    #print(current_player_legal_moves)
    return random.choice(current_player_legal_moves)

def can_t_move(pos) :
    if pos.legal_moves.count() == 0 :
        return True
    else :
        return False

def child_of_position(pos) :
    '''Détermine toutes les possitions filles d'une position (avec leur coup associé pour aller plus vite).'''
    current_player_legal_moves = [move for move in pos.legal_moves]
    result = []
    for i in range(len(current_player_legal_moves)-1) :
        new_pos = chess.Board(pos.fen())
        new_pos.push_san(str(current_player_legal_moves[i]))
        result.append((new_pos, str(current_player_legal_moves[i])))
    return result  # (board, move)

def minimax(pos, depth, alpha, beta, maximizingPlayer) :
    '''Algorithme MiniMax avec Alpha-Bêta.'''
    # maximizingPlayer = Trus si White2Play et False si Black2Play

    if depth == 0 or game_is_finished(pos[0]) or can_t_move(pos[0]) :
        return (evaluate(pos[0]), None)

    one_legal_move = one_legal(pos[0])
    if maximizingPlayer :
        maxEval = (-infinity, one_legal_move) # None = move
        for child in child_of_position(pos[0]) :
            evalu = minimax(child, depth - 1, alpha, beta, False)
            maxEval = (max(maxEval[0], evalu[0]), maxEval[1])
            if max(maxEval[0], evalu[0]) == evalu[0] :
                maxEval = (max(maxEval[0], evalu[0]), child[1])
            alpha = max(alpha, evalu[0])
            if beta <= alpha :
                break
        return maxEval

    else :
        maxEval = (infinity, one_legal_move)
        for child in child_of_position(pos[0]) :
            evalu = minimax(child, depth - 1, alpha, beta, True)
            maxEval = (min(maxEval[0], evalu[0]), maxEval[1])
            if max(maxEval[0], evalu[0]) == evalu[0] :
                maxEval = (min(maxEval[0], evalu[0]), child[1])
            alpha = min(alpha, evalu[0])
            if beta <= alpha :
                break
        return maxEval


show('rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR', chess.Board())
