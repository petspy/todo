class Setting {
  static API_ENDPOINT = 'http://localhost:5000/api'

  // TODO: Implement the API_ENDPOINT getter to retrieve from configuration location, e.g. third party service or .env
  static getApiEndpoint() {
    return this.API_ENDPOINT
  }
}

export default Setting
