# 2D-Top-Down-Shooter---Teste

## Controles:

- **Movimentação** - WASD
- **Atirar** - Botão esquerdo do mouse ou Ctrl
- **Restart** - tecla 'R'

## Armas

- **Simple gun** - arma inicial, lenta e com pouco dano.
- **Dual gun** - arma com taxa de fogo mais alta.
- **Shotgun** - arma com um alcance baixo, mas com multiplos desparos.
- **Sniper** - arma com um dano muito alto, porém com uma taxa de fogo muito baixa.

## IA (State Machine)

- **Wander** - a IA irá escolher um destino aleatório para seguir.
- **Awareness** - quando o jogador entra na área de detecção, a IA irá procurá-lo até entrar no campo de visão ou sair da área de detecção.
- **Chase** - quando o jogador está no campo de visão, a IA irá se mover em direção ao mesmo até que chegue na distâcia de atirar.
- **Shoot** - enquanto o jogador estiver na distância de atirar, a IA irá mirar no jogador e atirar.
