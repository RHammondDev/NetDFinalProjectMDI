﻿
'-----------------------------------------------------------------
'Author: Robin Hammond
'Student Number: 100773359
'Date: August 4, 2020.
'Last Modified: July 29, 2020.
'Course: NETD-2202-01 Net Development I
'Assignment: Final Project
'File Name: ParentForm.vb
'Description: In this lab we created a Multi-Document text editing application 
'from scratch. We created a form where users can create new files,
'open existing files and save existing or new files. We also created
'editing utilities for copy, cut and paste in the application.
'An about button was also created which displays a form with information
'about the creator of the application.
'-----------------------------------------------------------------

Option Strict On
Imports System.IO

Public Class ParentForm

#Region "Variables"
	Dim docLocation As String = String.Empty 'This is a variable to hold the file location
	Dim docName As String  'This holds the document name
	Dim txtEdit As String = "Multi-Document Text Editor 2.0 - " 'This variable holds the name of the application for the title bar


#End Region

#Region "Event Handlers"
	' NEW
	Private Sub mnuNewClick(sender As Object, e As EventArgs) Handles mnuNew.Click
		NewFile()
		docLocation = String.Empty
		lblStatus.Text = "New File Started"
		Me.ActiveMdiChild.Text = "Untitled"
	End Sub
	' OPEN
	Private Sub mnuOpenClick(sender As Object, e As EventArgs) Handles mnuOpen.Click, tsbOpenFile.Click
		Dim fileReader As String

		If openDialog.ShowDialog() = DialogResult.OK Then
			NewFile()
			docName = openDialog.SafeFileName
			docLocation = openDialog.FileName
			fileReader = My.Computer.FileSystem.ReadAllText(docLocation)
			Me.ActiveMdiChild.Text = docName
			Me.ActiveMdiChild.ActiveControl.Text = fileReader
			lblStatus.Text = docName + " has been opened"
		End If
	End Sub
	' SAVE NEEDS REWRITE
	Private Sub mnuSaveClick(sender As Object, e As EventArgs) Handles mnuSave.Click
		'Dim saveDialog As New SaveFileDialog
		'Dim saveStream As StreamWriter
		'Dim activeChild As ChildForm = DirectCast(Me.ActiveMdiChild, ChildForm)

		saveDialog.Filter = "Txt Files| *.txt | All Files| *.*"

		If docLocation = String.Empty Then
			If saveDialog.ShowDialog() = DialogResult.OK Then
				docLocation = saveDialog.FileName
				FileSaved(docLocation)
				lblStatus.Text = "File Saved Successfully - " & openDialog.FileName
			End If

		Else
			FileSaved(docLocation)
			lblStatus.Text = "File Saved Successfully - " & openDialog.FileName
		End If





		'If docLocation = String.Empty Then
		'If saveDialog.ShowDialog = DialogResult.OK Then
		'docLocation = saveDialog.FileName
		'FileSaved(docLocation)
		'lblStatus.Text = "File Saved Successfully - " & openDialog.FileName
		'End If
		'Else
		'FileSaved(docLocation)
		'lblStatus.Text = "File Saved Successfully - " & openDialog.FileName
		'End If
	End Sub
	' SAVE AS
	Private Sub mnuSaveAsClick(sender As Object, e As EventArgs) Handles mnuSaveAs.Click
		Dim saveDialog As New SaveFileDialog
		Dim saveStream As StreamWriter
		Dim activeChild As ChildForm = DirectCast(Me.ActiveMdiChild, ChildForm)

		saveDialog.Filter = "Txt Files| *.txt | All Files| *.*"

		If saveDialog.ShowDialog() = DialogResult.OK Then
			saveStream = New StreamWriter(saveDialog.FileName)
			saveStream.Write(activeChild.txtDisplay.Text)
			saveStream.Close()

			lblStatus.Text = "File Saved Successfully - " & openDialog.FileName
		End If
	End Sub
	' CLOSE
	Private Sub mnuCloseClick(sender As Object, e As EventArgs) Handles mnuClose.Click
		Me.ActiveMdiChild.Close()
	End Sub
	' EXIT
	Private Sub mnuExitClick(sender As Object, e As EventArgs) Handles mnuExit.Click
		Application.Exit()
	End Sub
	' CUT
	Private Sub mnuCutClick(sender As Object, e As EventArgs) Handles mnuCut.Click
		If Not Me.ActiveMdiChild Is Nothing Then
			DirectCast(Me.ActiveMdiChild.ActiveControl, TextBox).Cut()
			lblStatus.Text = "Cut"
		End If
	End Sub
	' COPY
	Private Sub mnuCopyClick(sender As Object, e As EventArgs) Handles mnuCopy.Click
		If Not Me.ActiveMdiChild Is Nothing Then
			DirectCast(Me.ActiveMdiChild.ActiveControl, TextBox).Copy()
			lblStatus.Text = "Copied"
		End If
	End Sub
	' PASTE
	Private Sub mnuPasteClick(sender As Object, e As EventArgs) Handles mnuPaste.Click
		If Not Me.ActiveMdiChild Is Nothing Then
			DirectCast(Me.ActiveMdiChild.ActiveControl, TextBox).Paste()
			lblStatus.Text = "Pasted"
		End If
	End Sub
	' CASCADE
	Private Sub mnuCascadeClick(sender As Object, e As EventArgs) Handles mnuCascade.Click
		Me.LayoutMdi(MdiLayout.Cascade)
		lblStatus.Text = "Open file windows have been cascaded"
	End Sub
	' TILE VERTICAL
	Private Sub mnuTileVerticalClick(sender As Object, e As EventArgs) Handles mnuTileVertical.Click
		Me.LayoutMdi(MdiLayout.TileVertical)
		lblStatus.Text = "Open file windows have been tiled vertically"
	End Sub
	' TILE HORIZTONAL
	Private Sub mnuTileHorizontalClick(sender As Object, e As EventArgs) Handles mnuTileHorizontal.Click
		Me.LayoutMdi(MdiLayout.TileHorizontal)
		lblStatus.Text = "Open file windows have been tiled horizontally"
	End Sub
	' ABOUT
	Private Sub mnuAboutClick(sender As Object, e As EventArgs) Handles mnuAbout.Click
		Dim aboutModal As New aboutForm
		aboutModal.ShowDialog()
	End Sub

#End Region

#Region "Functions and Sub Procedures"
	'This Sub Routine handles saving files
	Public Sub FileSaved(ByVal file As String)
		Dim outputStream As StreamWriter
		outputStream = New StreamWriter(file)
		docName = Path.GetFileName(file)
		outputStream.Write(Me.ActiveMdiChild.ActiveControl.Text)
		outputStream.Close()
		Me.ActiveMdiChild.Text = docName + " - " + file
	End Sub

	Public Sub NewFile()
		Dim newDocument As New ChildForm
		newDocument.MdiParent = Me
		newDocument.Show()
	End Sub

#End Region




End Class