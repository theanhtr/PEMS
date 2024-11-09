import axios from 'axios'
import { success, failure } from '../service/HttpResponse.js'
import store from '../store/index.js'

const TTANHAxios = axios.create({
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json',
    ContentLanguage: store.state.langCode,
    Authorization: `Bearer ${localStorage.getItem('userToken')}`
  },
  withCredentials: true // gửi cookie, session lên server
})

TTANHAxios.interceptors.response.use(
  function (response) {
    return success(response)
  },
  function (error) {
    return failure(error.response)
  }
)

export default TTANHAxios
