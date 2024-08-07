import axios from 'axios'

import Setting from '../configurations/Setting'

export const axiosInstance = initialiseAxiosContext()

function initialiseAxiosContext() {
  const instance = axios.create({
    baseURL: Setting.getApiEndpoint(),
  })

  async function axiosInterceptor(config) {
    config.headers = config.headers || {}

    // TODO: Implement the interceptor to add the Authorization header
    if (sessionStorage.getItem('accessToken')) {
      config.headers.Authorization = `Bearer ${sessionStorage.getItem('accessToken')}`
    }

    return config
  }

  instance.interceptors.request.use(axiosInterceptor)

  return instance
}

export default axiosInstance
