import { DownloadFile } from './DownloadFile';

function renderFileList(files) {
  return (
    <table>
      <thead>
        <tr>
          <td><b>Files</b></td>
        </tr>
      </thead>
      <tbody>
        {files.map(file =>
          <tr key={file}>
            <td><DownloadFile file={file} /></td>
          </tr>
        )}
      </tbody>
    </table>
  )
}

function FileList({ files }) {
  return files === undefined ? (<p><em>Loading...</em></p>) : renderFileList(files);
}

export default FileList
