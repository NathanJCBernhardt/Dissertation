import chess
import chess.engine
import random
import numpy
import pygame
import asyncio

pygame.init()
screen = pygame.display.set_mode((480, 480))
pygame.display.set_caption("Chess Engine")
screen.fill("WHITE")



WHITE = (255, 255, 255)
RED = (255, 0, 0)

chessboard = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\chessboard.png').convert_alpha()
King = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\whiteKing.png').convert_alpha()
king = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\blackKing.png').convert_alpha()
Knight = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\whiteKnight.png').convert_alpha()
knight = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\blackKnight.png').convert_alpha()
Rook = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\whiteRook.png').convert_alpha()
rook = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\blackRook.png').convert_alpha()
Queen = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\whiteQueen.png').convert_alpha()
queen = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\blackQueen.png').convert_alpha()
Bishop = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\whiteBishop.png').convert_alpha()
bishop = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\blackBishop.png').convert_alpha()
Pawn = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\whitePawn.png').convert_alpha()
pawn = pygame.image.load(r'C:\Users\Nathan\Documents\project\dissertation\PythonAI\icons\blackPawn.png').convert_alpha()

# this function will create our x (board)
def random_board(max_depth=200):
  board = chess.Board()
  depth = random.randrange(0, max_depth)

  for _ in range(depth):
    all_moves = list(board.legal_moves)
    random_move = random.choice(all_moves)
    board.push(random_move)
    if board.is_game_over():
      break

  return board

def draw(fen, col, rank, board):
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
  
async def main(board) -> None:
  transport, engine = await chess.engine.popen_uci(r"C:\Users\Nathan\Documents\project\dissertation\Unity\DissertationProject\Assets\stockfish_14.1_win_x64_avx2\stockfish_14.1_win_x64_avx2.exe")
  print(board)
  k = 0
  while not board.is_game_over():
    for event in pygame.event.get():
      if event.type == pygame.QUIT:
          pygame.quit()
          sys.exit()
    while k == 0:
        ans = input()
        if chess.Move.from_uci(ans) in board.legal_moves:
            k = 1
            board.push(chess.Move.from_uci(ans))
        else:
            print("Illegal move!")
            k = 0
    print(board)
    show(board.fen(), board)
    result = await engine.play(board, chess.engine.Limit(time=0.01))
    print(result)
    info = await engine.analyse(board, chess.engine.Limit(time=0.01))
    print("Score:", info["score"])
    board.push(result.move)
    print(board)
    show(board.fen(), board)
    k = 0
  await engine.quit()
  

board = chess.Board()
show(board.fen(), board)

asyncio.set_event_loop_policy(chess.engine.EventLoopPolicy())
asyncio.run(main(board))


