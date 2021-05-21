pipeline {
	agent {
		node {
			label "Slave05"
			customWorkspace "D:/JenkinsUAT/workspace/Internship_2019S_DoubleDev_devops"
		}
	}
	environment {
		nuget = 'C:/Program Files (x86)/NuGet/nuget.exe'
		nunit3 = 'C:/Program Files (x86)/NUnit.org/nunit-console/nunit3-console.exe'
		openCover = 'D:/Tools/OpenCover/OpenCover.Console.exe'
	}
	stages {
		stage("Build") {
			steps {
				dir("MolDavaBanking") {
					script {
						bat "\"${nuget}\" restore MolDavaBanking.sln"
						bat "\"${tool 'MSBuild 2017 (15.0) on Slave05'}\" /p:OutputPath=D:/JenkinsUAT/workspace/Internship_2019S_DoubleDev_devops/lib /p:Configuration=Release MolDavaBanking.sln"
					}
				}
			}
		}
		stage("SonarQube Analysis") {
			steps {
				dir("MolDavaBanking") {
					withSonarQubeEnv('Endava Sonar UAT') {
						bat "\"${tool 'SonarUAT Scan Fwk(4.5)'}\"\\SonarScanner.MSBuild.exe begin /k:com.endava:bankingDoubleDev /n:BankingProjectInternshipDoubleDev /v:1.0 /d:sonar.cs.nunit.reportsPaths=NUnitResults.xml /d:sonar.cs.opencover.reportsPaths=Coverage.xml /d:sonar.exclusions=**/*.js,**/*.css,**/*.*html,**/*.ts,**/*.less"
						bat "\"${tool 'MSBuild 2017 (15.0) on Slave05'}\" /t:Rebuild"
						bat "\"${nunit3}\" --result=NUnitResults.xml MolDavaBanking.Tests/bin/Debug/MolDavaBanking.Tests.dll"
						bat "\"${openCover}\" -output:Coverage.xml \"-register:user\" -target:\"${nunit3}\" -targetargs:\"MolDavaBanking.Tests/bin/Debug/MolDavaBanking.Tests.dll --config=Debug --result=NUnitResults.xml;format=nunit3\""
						bat "\"${tool 'SonarUAT Scan Fwk(4.5)'}\"\\SonarScanner.MSBuild.exe end"
					}
				}
			}
		}
		stage("NugetPackege") {
			steps {
				script {
					bat "\"${nuget}\" pack doubledev.nuspec -IncludeReferencedProjects -Version 1.0.0-\"${currentBuild.number}\" -Properties Configuration=Release"
				}
			}
		}
		stage("Nexus Deploy") {
			steps {
				script {
					bat "\"${nuget}\" setapikey f1a73c8c-8693-3f29-aa45-d82486ccc4a8 -Source https://nexus-uat.endava.net/repository/BankingProject_2/"
					bat "\"${nuget}\" push DoubleDev.1.0.0-\"${currentBuild.number}\".nupkg f1a73c8c-8693-3f29-aa45-d82486ccc4a8 -Source https://nexus-uat.endava.net/repository/BankingProject_2/"
				}
				stash name: "DoubleDev", includes: "*.nupkg"
			}
		}
		stage("Deploy to EC2") {
			steps {
				script {
					node("Slave06") {
						unstash "DoubleDev"
						sh "unzip -o DoubleDev.1.0.0-\"${currentBuild.number}\".nupkg -d DoubleDev"
						withCredentials([usernamePassword(credentialsId: "icrudu_EC2", passwordVariable: "user_pass", usernameVariable: "user_name")]) {
							sh """
								sshpass -p "${user_pass}" scp -o StrictHostKeyChecking=no -r DoubleDev ${user_name}@3.208.127.208:/cygdrive/c/inetpub/wwwroot
							"""
						}
					}
				}
			}
		}
	}
}