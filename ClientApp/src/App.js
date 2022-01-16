import React, { useState, useEffect } from 'react';
import axios from 'axios'
import { FilePicker } from 'react-file-picker'
import FileList from './components/FileList'

function uploadFile(putFiles) {
  return async (file) => {
    console.log(file)
    const data = new FormData()
    data.append('FormFile', file)
    try {
      const res = await axios.post('/filesave', data)
      console.log(res)
    } catch (e) {
      console.log(e)
      alert('failed to upload file')
    }
    populateFileList(putFiles)();
  }
}

function populateFileList(putFiles) {
  return async () => {
    const response = await fetch('filelist')
    const files = await response.json()
    console.log('there are', files.fileNames.length, 'files')
    putFiles(files.fileNames)
  }
}

function App() {
  const [getFiles, putFiles] = useState([])
  useEffect(() => populateFileList(putFiles)(), []);

  return (
    <>
    <br />
    <div style={{display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
      <FilePicker onChange={uploadFile(putFiles)}>
        <button>
          Upload a file
        </button>
      </FilePicker>
    </div>
      <br />
    <div style={{display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
      <FileList files={getFiles}/>
    </div>
    </>
  )
}

export default App
