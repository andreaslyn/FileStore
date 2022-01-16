import React, { Component } from 'react';

export class DownloadFile extends Component {
  static displayName = DownloadFile.name;

  constructor(props) {
    super(props)
    this.state = { file: props.file }
  }

  render() {
    const file = this.state.file
    return <a href={'/filedownload/' + file} download>{file}</a>
  }
}
