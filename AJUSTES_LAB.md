# Ajustes ao rodar em outro computador (ex: laboratório da faculdade)

Guia rápido caso o projeto não compile num PC diferente do seu, por causa
da versão do .NET/workload MAUI instalada.

## 1. Sintoma esperado

Ao tentar compilar (`Recompilar` ou `Executar`), o Visual Studio mostra um
erro parecido com:

```
error NETSDK1045: A versão atual do .NET SDK não é compatível...
```
ou
```
The specified framework 'Microsoft.Android' version '9.0.x' was not found.
```

Isso significa que o SDK/workload do .NET instalado naquele computador não
inclui a versão que o projeto está pedindo (hoje: **.NET 9**, veja o passo 2).

## 2. Diagnóstico rápido

Abra um terminal (ou o **"Prompt de Desenvolvedor"** dentro do próprio
Visual Studio, em Ferramentas → Prompt de Comando) e rode:

```
dotnet --list-sdks
```

Isso lista as versões de SDK instaladas naquele PC. Se não aparecer
nenhuma linha começando com `9.0.`, é esse o problema.

## 3. Correção — trocar a versão alvo do projeto

Abra o arquivo:

```
SistemaVeterinario/SistemaVeterinario.csproj
```

Localize a linha (perto do topo, dentro do primeiro `<PropertyGroup>`):

```xml
<TargetFrameworks>net9.0-android;net9.0-ios;net9.0-maccatalyst</TargetFrameworks>
```

Troque `net9.0-` pela versão disponível naquele PC (ex: se o `dotnet --list-sdks`
mostrou `8.0.x`, use `net8.0-`):

```xml
<TargetFrameworks>net8.0-android;net8.0-ios;net8.0-maccatalyst</TargetFrameworks>
```

Se o PC tiver .NET 8, também ajuste a linha logo abaixo (Windows), caso exista:

```xml
<TargetFrameworks Condition="$([MSBuild]::IsOSPlatform('windows'))">$(TargetFrameworks);net8.0-windows10.0.19041.0</TargetFrameworks>
```

**Não precisa mexer em mais nada.** O projeto não usa nenhum recurso
exclusivo do .NET 9 — é só essa troca de número de versão mesmo.

## 4. Depois de editar

1. Botão direito na Solução → **Restaurar Pacotes NuGet**.
2. Botão direito no projeto → **Recompilar**.
3. Rode normalmente no emulador/dispositivo (▶ Executar).

## 5. Importante

- Essa mudança é **só local, no PC do laboratório** — não precisa
  commitar/enviar essa alteração pro GitHub, é só pra aquele computador
  específico funcionar. No seu PC de casa, o projeto continua em .NET 9
  normalmente.
- Se mesmo assim não compilar, o próximo passo é conferir se o **workload
  MAUI** está instalado no Visual Studio Installer daquele PC
  (Cargas de Trabalho → "Desenvolvimento de .NET Multi-Platform App UI").
