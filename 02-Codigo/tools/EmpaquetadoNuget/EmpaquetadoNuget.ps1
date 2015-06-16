Param (
	[switch]$Publicar,
    [string]$Ambiente,
	[string]$ApiKey
)
$ErrorActionPreference = "Stop"
$global:ExitCode = 1

function Escribir-Log {

	#region Parameters
	
		[cmdletbinding()]
		Param(
			[Parameter(ValueFromPipeline=$true)]
			[array] $Messages,

			[Parameter()] [ValidateSet("Error", "Warn", "Info")]
			[string] $Level = "Info",
			
			[Parameter()]
			[Switch] $NoConsoleOut = $false,
			
			[Parameter()]
			[String] $ForegroundColor = 'White',
			
			[Parameter()] [ValidateRange(1,30)]
			[Int16] $Indent = 0,

			[Parameter()]
			[IO.FileInfo] $Path = ".\NuGet.log",
			
			[Parameter()]
			[Switch] $Clobber,
			
			[Parameter()]
			[String] $EventLogName,
			
			[Parameter()]
			[String] $EventSource,
			
			[Parameter()]
			[Int32] $EventID = 1
			
		)
		
	#endregion

	Begin {}

	Process {
		
		$ErrorActionPreference = "Continue"

		if ($Messages.Length -gt 0) {
			try {			
				foreach($m in $Messages) {			
					if ($NoConsoleOut -eq $false) {
						switch ($Level) {
							'Error' { 
								Write-Error $m -ErrorAction SilentlyContinue
								Write-Host ('{0}{1}' -f (" " * $Indent), $m) -ForegroundColor Red
							}
							'Warn' { 
								Write-Warning $m 
							}
							'Info' { 
								Write-Host ('{0}{1}' -f (" " * $Indent), $m) -ForegroundColor $ForegroundColor
							}
						}
					}

					if ($m.Trim().Length -gt 0) {
						$msg = '{0}{1} [{2}] : {3}' -f (" " * $Indent), (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $Level.ToUpper(), $m
	
						if ($Clobber) {
							$msg | Out-File -FilePath $Path -Force
						} else {
							$msg | Out-File -FilePath $Path -Append
						}
					}
			
					if ($EventLogName) {
			
						if (-not $EventSource) {
							$EventSource = ([IO.FileInfo] $MyInvocation.ScriptName).Name
						}
			
						if(-not [Diagnostics.EventLog]::SourceExists($EventSource)) { 
							[Diagnostics.EventLog]::CreateEventSource($EventSource, $EventLogName) 
						} 

						$log = New-Object System.Diagnostics.EventLog  
						$log.set_log($EventLogName)  
						$log.set_source($EventSource) 
				
						switch ($Level) {
							"Error" { $log.WriteEntry($Message, 'Error', $EventID) }
							"Warn"  { $log.WriteEntry($Message, 'Warning', $EventID) }
							"Info"  { $log.WriteEntry($Message, 'Information', $EventID) }
						}
					}
				}
			} 
			catch {
				throw "Failed to create log entry in: '$Path'. The error was: '$_'."
			}
		}
	}

	End {}

	<#
		.SYNOPSIS
			Writes logging information to screen and log file simultaneously.

		.DESCRIPTION
			Writes logging information to screen and log file simultaneously. Supports multiple log levels.

		.PARAMETER Messages
			The messages to be logged.

		.PARAMETER Level
			The type of message to be logged.
			
		.PARAMETER NoConsoleOut
			Specifies to not display the message to the console.
			
		.PARAMETER ConsoleForeground
			Specifies what color the text should be be displayed on the console. Ignored when switch 'NoConsoleOut' is specified.
		
		.PARAMETER Indent
			The number of spaces to indent the line in the log file.

		.PARAMETER Path
			The log file path.
		
		.PARAMETER Clobber
			Existing log file is deleted when this is specified.
		
		.PARAMETER EventLogName
			The name of the system event log, e.g. 'Application'.
		
		.PARAMETER EventSource
			The name to appear as the source attribute for the system event log entry. This is ignored unless 'EventLogName' is specified.
		
		.PARAMETER EventID
			The ID to appear as the event ID attribute for the system event log entry. This is ignored unless 'EventLogName' is specified.

		.EXAMPLE
			PS C:\> Write-Log -Message "It's all good!" -Path C:\MyLog.log -Clobber -EventLogName 'Application'

		.EXAMPLE
			PS C:\> Write-Log -Message "Oops, not so good!" -Level Error -EventID 3 -Indent 2 -EventLogName 'Application' -EventSource "My Script"

		.INPUTS
			System.String

		.OUTPUTS
			No output.
			
		.NOTES
			Revision History:
				2011-03-10 : Andy Arismendi - Created.
	#>
}

function Crear-Proceso() {
	param([string] $fileName, [string] $arguments)

	$pinfo = New-Object System.Diagnostics.ProcessStartInfo
	$pinfo.RedirectStandardError = $true
	$pinfo.RedirectStandardOutput = $true
	$pinfo.UseShellExecute = $false
	$pinfo.FileName = $fileName
	$pinfo.Arguments = $arguments

	$p = New-Object System.Diagnostics.Process
	$p.StartInfo = $pinfo

	return $p
}

function Publicar {

	Escribir-Log " "
	Escribir-Log "Publicando paquete..." -ForegroundColor Green

	# Get nuget config
	[xml]$nugetConfig = Get-Content 02-Codigo\tools\EmpaquetadoNuget\NuGet.Config
	
	$nugetConfig.configuration.pushRepos.repo | ForEach-Object {
		$url = $_.url
        $ambienteConf=$_.ambiente
		if ($Ambiente -eq $ambienteConf){
		Escribir-Log "Url del Repositorio: $url"
		Escribir-Log " Ambiente a publicar: $Ambiente "
		Escribir-Log " Ambiente configurado: $ambienteConf "
       
		Get-ChildItem *.nupkg | Where-Object { $_.Name.EndsWith(".symbols.nupkg") -eq $false } | ForEach-Object { 
         
			# Try to push package
			$tarea = Crear-Proceso 02-Codigo\tools\EmpaquetadoNuget\NuGet.exe ("push " + $_.Name + " -s " + $url + " " + $ApiKey + " -Verbosity Detailed")
			$tarea.Start() | Out-Null
			$tarea.WaitForExit()
			
			$salida = ($tarea.StandardOutput.ReadToEnd() -Split '[\r\n]') |? { $_ }
			$errorNuget = ($tarea.StandardError.ReadToEnd() -Split '[\r\n]') |? { $_ }
			Escribir-Log $salida
			Escribir-Log $errorNuget Error
		   
			if ($tarea.ExitCode -gt 0) {
				$global:ExitCode = 1
			}
			else {
				$global:ExitCode = 0
			}
          }                
		 
		}     
	}
}

Escribir-Log " "
Escribir-Log "Nubise Empaquetador NuGet 1.0.0" -ForegroundColor Yellow

# Asegurarse que se puede escribir el ejecutable NuGet
Set-ItemProperty 02-Codigo\tools\EmpaquetadoNuget\NuGet.exe -Name IsReadOnly -Value $false

# Asegurarse que los archivos nupkg se pueden escribir y crear respaldo
if (Test-Path *.nupkg) {
	Set-ItemProperty *.nupkg -Name IsReadOnly -Value $false

	Escribir-Log " "
	Escribir-Log "Creando respaldo..." -ForegroundColor Green

	Get-ChildItem *.nupkg | ForEach-Object { 
		Move-Item $_.Name ($_.Name + ".bak") -Force
		Escribir-Log ("Renombrado " + $_.Name + " a " + $_.Name + ".bak")
	}
}

Escribir-Log " "


Escribir-Log " "
Escribir-Log "Creando paquete..." -ForegroundColor Green

# Crear paquete de simbolos si existe algun .pdb en el directorio lib
If ((Get-ChildItem *.pdb -Path .\lib -Recurse).Count -gt 0) {
	$tareaEmpaquetado = Crear-Proceso 02-Codigo\tools\EmpaquetadoNuget\NuGet.exe ("pack 02-Codigo\tools\EmpaquetadoNuget\Package.nuspec -Symbol -Verbosity Detailed")
	$tareaEmpaquetado.Start() | Out-Null
	$tareaEmpaquetado.WaitForExit()
			
	$salida = ($tareaEmpaquetado.StandardOutput.ReadToEnd() -Split '[\r\n]') |? {$_}
	$errorNuget = (($tareaEmpaquetado.StandardError.ReadToEnd() -Split '[\r\n]') |? {$_}) 
	Escribir-Log $salida
	Escribir-Log $errorNuget Error

	$global:ExitCode = $tareaEmpaquetado.ExitCode
}
Else {

	$tareaEmpaquetado = Crear-Proceso 02-Codigo\tools\EmpaquetadoNuget\NuGet.exe ("pack 02-Codigo\tools\EmpaquetadoNuget\Package.nuspec -Verbosity Detailed")
	$tareaEmpaquetado.Start() | Out-Null
	$tareaEmpaquetado.WaitForExit()			
	$salida = ($tareaEmpaquetado.StandardOutput.ReadToEnd() -Split '[\r\n]') |? {$_}
	$errorNuget = (($tareaEmpaquetado.StandardError.ReadToEnd() -Split '[\r\n]') |? {$_}) 
	Escribir-Log $salida
	Escribir-Log $errorNuget Error

	$global:ExitCode = $tareaEmpaquetado.ExitCode
}

# Check if package should be published
if ($Publicar -and $global:ExitCode -eq 0) {
	Publicar
}

Escribir-Log " "
Escribir-Log "Codigo de Salida: $global:ExitCode" -ForegroundColor Gray

$host.SetShouldExit($global:ExitCode)
Exit $global:ExitCode