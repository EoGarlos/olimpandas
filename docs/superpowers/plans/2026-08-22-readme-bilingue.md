# Bilingual Project README Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Criar, validar e publicar um README informativo em português com resumo em inglês para o projeto acadêmico Olimpandas.

**Architecture:** A documentação ficará concentrada no `README.md` da raiz. O conteúdo principal em português descreverá somente o que existe no protótipo e separará o roadmap; uma seção final em inglês resumirá propósito, recursos, controles e execução.

**Tech Stack:** Markdown, Unity 6.5 (6000.5.9f1), C#, Git e GitHub CLI.

---

## File map

- Create: `README.md` - página inicial bilíngue do repositório.
- Reference: `ProjectSettings/ProjectVersion.txt` - versão exata do Unity.
- Reference: `Assets/PlayerController.cs` - comportamento e controles padrão.
- Reference: `Assets/Prefabs/Characters/PlayerBase.prefab` - controles do Jogador 1.
- Reference: `Assets/Scenes/PrototypeRace.unity` - controles sobrescritos do Jogador 2 e cena executável.
- Reference: `docs/superpowers/specs/2026-08-22-readme-bilingue-design.md` - critérios editoriais aprovados.

### Task 1: Criar o README bilíngue

**Files:**
- Create: `README.md`

- [ ] **Step 1: Executar a validação antes da implementação**

Run:

```powershell
$readme = 'README.md'
if (-not (Test-Path $readme)) { throw 'README.md ausente' }
$content = Get-Content -Raw $readme
$required = @(
  'projeto acadêmico',
  'foco em aprendizado',
  '## Estado atual',
  '## Controles',
  '## Como executar',
  '## Roadmap',
  '## English summary'
)
foreach ($item in $required) {
  if ($content -notmatch [regex]::Escape($item)) { throw "Seção ou texto ausente: $item" }
}
```

Expected: FAIL with `README.md ausente`.

- [ ] **Step 2: Criar o conteúdo completo**

Create `README.md` with:

```markdown
# Olimpandas

Olimpandas é um party game 2D competitivo desenvolvido em Unity e C#. Dois jogadores disputam minigames curtos, conquistam medalhas e tentam vencer uma série melhor de três.

> **Projeto acadêmico:** este repositório foi criado com foco em aprendizado e prática de desenvolvimento de jogos, programação em C#, Unity e colaboração com Git/GitHub. O jogo ainda está em desenvolvimento e não representa uma versão comercial ou finalizada.

## Visão geral

A proposta do Olimpandas é reunir provas rápidas e fáceis de entender em uma experiência PvP local. O primeiro recorte jogável é uma corrida de plataforma para dois jogadores, usada para validar o loop principal: iniciar a prova, competir, identificar o vencedor, entregar uma medalha e avançar para a rodada seguinte.

## Estado atual

O protótipo atual possui:

- PvP local para dois jogadores;
- movimentação, pulo e dash independentes;
- câmera compartilhada com enquadramento dinâmico;
- pista de corrida com plataformas e obstáculos;
- checkpoints, zonas de queda e respawn individual;
- detecção da linha de chegada e do vencedor;
- medalhas persistentes entre rodadas;
- partida melhor de três;
- interface de resultado e opção de próxima prova ou nova partida;
- base inicial para personagem animado e prefabs reutilizáveis em desenvolvimento.

## Controles

| Ação | Jogador 1 | Jogador 2 |
| --- | --- | --- |
| Mover para a esquerda | `A` | `Seta para a esquerda` |
| Mover para a direita | `D` | `Seta para a direita` |
| Pular | `W` | `Seta para cima` |
| Dash | `Shift esquerdo` | `Shift direito` |

## Tecnologias

- Unity `6000.5.9f1`;
- C#;
- física e ferramentas 2D da Unity;
- TextMesh Pro e Unity UI;
- Git e GitHub.

## Como executar

### Requisitos

- Unity Hub;
- Unity Editor `6000.5.9f1` ou uma versão compatível da mesma linha;
- Git.

### Passos

```bash
git clone https://github.com/patrckmello/olimpandas.git
cd olimpandas
```

1. No Unity Hub, selecione **Add > Add project from disk**.
2. Escolha a pasta clonada do projeto.
3. Abra o projeto com a versão indicada do Unity.
4. Abra a cena `Assets/Scenes/PrototypeRace.unity`.
5. Pressione **Play** no Unity Editor.

Não há uma build distribuída neste repositório no momento.

## Estrutura do projeto

```text
Assets/
  Art/              Arte, personagens, mapas, interface e efeitos
  Audio/            Música e efeitos sonoros
  Prefabs/          Objetos reutilizáveis do jogo
  Scenes/           Cenas jogáveis e protótipos
  Scripts/          Organização planejada dos scripts por sistema
Packages/           Dependências do projeto Unity
ProjectSettings/    Configurações versionadas do Unity
```

Parte da organização técnica ainda está em andamento; alguns scripts permanecem diretamente em `Assets/` enquanto são migrados para a estrutura definitiva.

## Roadmap

- consolidar prefabs e a organização dos scripts;
- concluir a integração do Panda-gigante como personagem mestre;
- implementar os poderes Stun e Slow;
- adicionar Arco e Flecha e Curling;
- criar seleção de personagens e expandir o elenco;
- substituir placeholders por arte e mapas com Tilemap;
- avaliar Snowboard e Pesca conforme o cronograma;
- considerar multiplayer online e WebGL somente após estabilizar o jogo local.

## Limitações atuais

- o projeto é um protótipo acadêmico em desenvolvimento;
- o modo disponível é PvP local;
- há apenas uma prova jogável;
- arte, áudio, organização interna e balanceamento ainda podem mudar;
- não há sistema de contas, backend, matchmaking ou ranking online.

---

## English summary

Olimpandas is a competitive 2D party game built with Unity and C#. Two local players compete in short minigames, earn medals, and try to win a best-of-three match.

> **Academic project:** this repository was created with a focus on learning and practicing game development, C#, Unity, and Git/GitHub collaboration. The game is still under development and is not a commercial or finished release.

### Current features

- two-player local PvP;
- independent movement, jumping, and dashing;
- dynamic shared camera;
- prototype platform race with obstacles;
- checkpoints and individual respawning;
- finish-line and winner detection;
- persistent medals and best-of-three match flow;
- result screen and round restart flow.

### Controls

| Action | Player 1 | Player 2 |
| --- | --- | --- |
| Move left | `A` | `Left Arrow` |
| Move right | `D` | `Right Arrow` |
| Jump | `W` | `Up Arrow` |
| Dash | `Left Shift` | `Right Shift` |

### Run the project

1. Clone this repository.
2. Add the cloned folder to Unity Hub.
3. Open it with Unity `6000.5.9f1` or a compatible version.
4. Open `Assets/Scenes/PrototypeRace.unity`.
5. Press **Play** in the Unity Editor.

No standalone build is currently distributed in this repository.
```

- [ ] **Step 3: Executar a validação de conteúdo novamente**

Run:

```powershell
$readme = 'README.md'
if (-not (Test-Path $readme)) { throw 'README.md ausente' }
$content = Get-Content -Raw $readme
$required = @(
  'projeto acadêmico',
  'foco em aprendizado',
  '## Estado atual',
  '## Controles',
  '## Como executar',
  '## Roadmap',
  '## English summary'
)
foreach ($item in $required) {
  if ($content -notmatch [regex]::Escape($item)) { throw "Seção ou texto ausente: $item" }
}
```

Expected: PASS with exit code `0` and no output.

- [ ] **Step 4: Validar formatação e fatos do repositório**

Run:

```powershell
git diff --check -- README.md
rg -n '6000\.5\.9f1' ProjectSettings/ProjectVersion.txt README.md
rg -n 'leftKey: 97|rightKey: 100|jumpKey: 119|dashKey: 304' Assets/Prefabs/Characters/PlayerBase.prefab
rg -n -C 1 'propertyPath: (leftKey|rightKey|jumpKey|dashKey)|value: (276|275|273|303)' Assets/Scenes/PrototypeRace.unity
```

Expected: `git diff --check` exits `0`; version and all eight control bindings are found.

- [ ] **Step 5: Commitar somente o README**

Run:

```powershell
git add -- README.md
git diff --cached --name-only
git commit -m "docs: add bilingual project readme"
```

Expected: staged output contains only `README.md`; commit succeeds.

### Task 2: Repetir a auditoria de segurança e publicar

**Files:**
- Verify: complete Git history and current worktree

- [ ] **Step 1: Confirmar que nenhum arquivo local do usuário entrou nos commits de documentação**

Run:

```powershell
git status --short --branch
git show --stat --oneline HEAD
git diff --name-only origin/main..HEAD
```

Expected: the existing Unity worktree changes remain unstaged; commits ahead of `origin/main` contain only the design, plan, and README documentation files.

- [ ] **Step 2: Reexecutar busca por segredos no histórico que será publicado**

Run the repository scanner for private keys, AWS keys, GitHub tokens, Google API keys, Slack tokens, JWTs, credential assignments, and authenticated connection strings. Report only rule names and paths, never matched secret values.

Expected: nenhum segredo encontrado. The known commit-author email is allowed by the repository owner.

- [ ] **Step 3: Verificar integridade Git imediatamente antes da publicação**

Run:

```powershell
git diff --check origin/main..HEAD
git log --oneline --decorate origin/main..HEAD
```

Expected: no whitespace errors; the documentation commits are listed.

- [ ] **Step 4: Publicar a documentação**

Run:

```powershell
git push origin main
```

Expected: push succeeds without `--force`; no history rewrite is needed because the audit found no secrets.

- [ ] **Step 5: Verificar o estado remoto final**

Run:

```powershell
git fetch origin
git status --short --branch
git rev-parse HEAD
git rev-parse origin/main
```

Expected: `HEAD` and `origin/main` are identical; the user's pre-existing uncommitted Unity changes remain present.
